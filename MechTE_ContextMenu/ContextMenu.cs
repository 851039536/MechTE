using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;

namespace MechTE_ContextMenu
{
    [ComVisible(true)]
    //如果按文件类型，按以下设置
    //[COMServerAssociation(AssociationType.ClassOfExtension, ".xlsx", ".xls")]

    //设置对全部文件和目录可用
   // [COMServerAssociation(AssociationType.AllFiles), COMServerAssociation(AssociationType.Directory)]
    [COMServerAssociation(AssociationType.AllFiles)]
    [COMServerAssociation(AssociationType.Directory)]
    [COMServerAssociation(AssociationType.DesktopBackground)]
    [COMServerAssociation(AssociationType.DirectoryBackground)]
    [COMServerAssociation(AssociationType.Folder)]
    public class ContextMenu : SharpContextMenu
    {
        /// <summary>
        /// 判断菜单是否需要被激活显示
        /// </summary>
        /// <returns></returns>
        protected override bool CanShowMenu()
        {
            return true;
        }

        /// <summary>
        /// 创建一个菜单，包含菜单项，设置ICON
        /// </summary>
        /// <returns></returns>
        protected override ContextMenuStrip CreateMenu()
        {

            var menu = new ContextMenuStrip();
            //设定菜单项显示文字
            var item = new ToolStripMenuItem("MechTool");
            //添加监听事件
            // item.Click += Item_Click;
            //设置图像及位置
             // item.Image = Properties.Resources.logo;
            item.ImageScaling = ToolStripItemImageScaling.None;
            item.ImageTransparentColor = System.Drawing.Color.White;
            item.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;

            
            //设置次级菜单
            var subItemsInfo = new Dictionary<string, string>()
            {
                { "上传(工程)", "EngineeringMode.exe,uploadingEng" },
                { "下载(工程)", "EngineeringMode.exe,downloadEng" },
                { "SimpleHIDWrite", "EngineeringMode.exe,SimpleHIDWrite" },
                // { "卸载", "EngineeringMode.exe,unload" }
            };
            
            foreach (var kv in subItemsInfo)
            {
                var subItem = new ToolStripMenuItem(kv.Key);
                subItem.Click += (o, e) =>
                {
                    Item_Click(o, e, kv.Value);
                };
                item.DropDownItems.Add(subItem);
            } 

            menu.Items.Add(item);

            return menu;
        }


        //菜单动作
        public void Item_Click(object sender, EventArgs e, string arg)
        {
            //分割,文件名和传递参数
            var argStrings = arg.Split(',');
            var fileName = argStrings[0];
            var identify = argStrings[1];
            
            //获取当前dll所在路径
            var rootPath = getRootPath();
            //文件路径+文件名称组合
            var appFile = $@"{rootPath}\{fileName}";
            if (!File.Exists(appFile))
            {
                MessageBox.Show($"找不到程序路径:{Environment.NewLine}{appFile}", "出错了", MessageBoxButtons.OK);
                return;
            }
            //转换为列表，然后将fileName添加到列表中
            var paths = SelectedItemPaths.ToList();
            paths.Add(fileName);
            var args = string.Join(" ", paths);
            Process.Start(appFile,identify);        
        }

        //获取当前dll所在路径
        public string getRootPath()
        {
            // 获取当前程序集的代码基路径
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            // 创建一个UriBuilder对象，用于解析代码基路径
            var uri = new UriBuilder(codeBase);
            // 获取解析后的路径，并对路径中的特殊字符进行解码
            var path = Uri.UnescapeDataString(uri.Path);
            // 获取解析后的路径，并对路径中的特殊字符进行解码
            return Path.GetDirectoryName(path);
        }
    }
}