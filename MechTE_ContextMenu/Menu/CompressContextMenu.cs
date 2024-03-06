using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;

namespace MechTE_ContextMenu.Menu
{
    // <summary>
    // 文件压缩包上传下载
    // </summary>
    [ComVisible(true)]
    //如果按文件类型，按以下设置
    [COMServerAssociation(AssociationType.ClassOfExtension,".zip",".7z",".exe",".pdf")]
    [COMServerAssociation(AssociationType.Folder)]
    public class CompressContextMenu : SharpContextMenu //继承SharpContextMenu 
    {
        /// <summary>
        /// 判断菜单是否需要被激活显示1
        /// </summary>
        /// <returns></returns>
        protected override bool CanShowMenu()
        {
            return true;
        }

        /// <summary>
        /// 创建一个菜单，包含菜单项，设置ICON.
        /// </summary>
        /// <returns></returns>
        protected override ContextMenuStrip CreateMenu()
        {
            // 将菜单绑定到窗口或控件
            var menu = new ContextMenuStrip();
            //设定菜单项显示文字
            var item = new ToolStripMenuItem("SW文件传输");

            var imgPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //设置图像及位置
            item.Image = Image.FromFile(imgPath + @"/image/zip.png");
            item.ImageScaling = ToolStripItemImageScaling.None;
            item.ImageTransparentColor = Color.White;
            item.ImageAlign = ContentAlignment.MiddleLeft;


            //设置次级菜单 
            var subItemsInfo = new Dictionary<string,string>()
            {
                { "上传(工程)", "DesktopMenu.exe,TeUploadingEng" },
                { "上传(量产)无功能", "DesktopMenu.exe,xxx" },
                { "上传七牛云", "DesktopMenu.exe,QiNiuUpLoading" },
            };

            foreach (var kv in subItemsInfo)
            {
                var subItem = new ToolStripMenuItem(kv.Key,Image.FromFile(imgPath + @"/image/zip1.png"));
                subItem.Click += (o,e) => { Item_Click(o,e,kv.Value); };
                item.DropDownItems.Add(subItem);
            }

            menu.Items.Add(item);

            return menu;
        }


        /// <summary>
        /// 菜单动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="arg"></param>
        public void Item_Click(object sender,EventArgs e,string arg)
        {
            //分割,文件名和传递参数
            var argStrings = arg.Split(',');
            //DesktopMenu.exe
            var fileName = argStrings[0];
            var identify = argStrings[1];

            //获取当前dll所在路径
            var rootPath = Config.GetRootPath();
            //文件路径+文件名称组合
            var appFile = $@"{rootPath}\{fileName}";
            if (!File.Exists(appFile))
            {
                MessageBox.Show($"找不到程序路径:{Environment.NewLine}{appFile}","出错了",MessageBoxButtons.OK);
                return;
            }

            //转换为列表，然后将fileName添加到列表中
            //选中的路径
            var paths = SelectedItemPaths.ToList();
            paths.Add(identify);
            var args = string.Join(" ",paths);
            Process.Start(appFile,args);
        }

    }
}