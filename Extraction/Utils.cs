//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Extraction
//{
//    public static class Utils
//    {
//        /// <summary>
//        /// Classe para calcular o novo Centro
//        /// </summary>
//        /// <param name="groupCoordinators">Grupo de coordenada que serão calculadas</param>
//        /// <param name="im">Imagem para encontrar o novo centro</param>
//        /// <returns></returns>
//        public static Point CalculateNewCenter(List<Point> groupCoordinators, Bitmap im)
//        {
//            double distance;
//            double closestDistance = double.MaxValue;
//            double tempAvg;
//            Point value = Common.General.Utils.EmptyPoint;

//            foreach (Point possibleCenter in groupCoordinators)
//            {
//                distance = 0;
//                foreach (Point item in groupCoordinators)
//                    distance += CalculateDistance(item, possibleCenter, im);

//                tempAvg = distance / groupCoordinators.Count;

//                if (tempAvg < closestDistance)
//                {
//                    closestDistance = tempAvg;
//                    value = possibleCenter;
//                }
//            }

//            return value;
//        }

//        /// <summary>
//        /// Classe para calcular a distância entre um ponto que é centro e outro não centro
//        /// </summary>
//        /// <param name="notCenter">Ponto não centro</param>
//        /// <param name="center">Centro</param>
//        /// <param name="im">Imagem</param>
//        /// <returns></returns>
//        public static double CalculateDistance(Point notCenter, Point center, Bitmap im)
//        {
//            if (im == null)
//                throw new Exception("Imagem nula ao tentar calcular a distância");

//            Color pixel = im.GetPixel(notCenter.X, notCenter.Y);
//            Color centerPixel = im.GetPixel(center.X, center.Y);

//            return Common.General.Utils.CalculateDistance(pixel, centerPixel);
//        }
//    }
//}
