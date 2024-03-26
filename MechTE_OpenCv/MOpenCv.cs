using OpenCvSharp;

namespace MechTE_OpenCv
{
    public static class MOpenCv
    {
        /// <summary>
        /// 获取图像中间位置的RGB
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadRGB(string path)
        {
            var src = Cv2.ImRead(path, ImreadModes.Color); // 读取彩色图片

            // 确保图片成功加载
            if (src.Empty())
            {
                return "[False]没有找到图片";
            }

            // 获取图像的宽度和高度  
            int width = src.Cols;
            int height = src.Rows;

            // 指定要检查的像素位置（例如，中心像素）  
            int x = width / 2;
            int y = height / 2;

            // 获取像素的颜色（BGR 格式）  
            var color = src.At<Vec3b>(y, x);
            // 输出颜色值  
            return $"B={color[0]}, G={color[1]}, R={color[2]}";
        }
    }
}