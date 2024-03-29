﻿using System;
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
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.DesktopBackground)]
    [COMServerAssociation(AssociationType.DirectoryBackground)]
    public class SystemContextMenu : SharpContextMenu
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
            var cuPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var menu = new ContextMenuStrip();
            //设定菜单项标题
            var item = new ToolStripMenuItem("SW系统工具(D)");
            //设置图像及位置
            item.Image = Image.FromFile(cuPath + @"/image/sw.png");
            item.ImageScaling = ToolStripItemImageScaling.None;
            item.ImageTransparentColor = Color.White;
            item.ImageAlign = ContentAlignment.MiddleLeft;
            
            //设置次级菜单
            var subItemsInfo = new Dictionary<string, string>();
            string[] strArray= File.ReadAllLines(cuPath + @"/config/SystemConfig.txt");	
            string fName = "DesktopMenu.exe,";
            foreach (var t in strArray)
            {
                //0图片路径 1 子菜单名称 2 子菜单参数
                var meunText = t.Split(',');
                //设置次级菜单
                subItemsInfo.Add(meunText[1],fName + meunText[2]);
                // MechWin.MesBoxs(meunText[1], meunText[2]);
                //传入键和图片
                var subItem = new ToolStripMenuItem(meunText[1], Image.FromFile(cuPath + @"/image/"+meunText[0]));
                subItem.Click += (o, e) => { Item_Click(o, e, fName +meunText[2]); };
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
            var rootPath = Config.GetRootPath();
            //文件路径+文件名称组合
            var appFile = $@"{rootPath}\{fileName}";
            if (!File.Exists(appFile))
            {
                MessageBox.Show($"找不到程序路径:{Environment.NewLine}{appFile}", "出错了", MessageBoxButtons.OK);
                return;
            }

            //转换为列表，然后将fileName添加到列表中
            var paths = SelectedItemPaths.ToList();
            paths.Add(identify);
            paths.Add(identify);
            var args = string.Join(" ", paths);
            Process.Start(appFile, args);
        }

       
    }
}