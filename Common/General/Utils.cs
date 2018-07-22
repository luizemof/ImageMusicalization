using Common.Extraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.General
{
    /// <summary>
    /// Classe para representar método úties e comum
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Ponto vazio (-1, -1)
        /// </summary>
        public static Point EmptyPoint { get { return new Point(-1, -1); } }

        /// <summary>
        /// Classe para calcular o novo Centro
        /// </summary>
        /// <param name="groupCoordinators">Grupo de coordenada que serão calculadas</param>
        /// <param name="im">Imagem para encontrar o novo centro</param>
        /// <returns></returns>
        public static Point CalculateNewCenter(List<Point> groupCoordinators, Bitmap im)
        {
            double distance;
            double closestDistance = double.MaxValue;
            double tempAvg;
            Point value = Utils.EmptyPoint;

            foreach (Point possibleCenter in groupCoordinators)
            {
                distance = 0;
                foreach (Point item in groupCoordinators)
                    distance += CalculateDistance(item, possibleCenter, im);

                tempAvg = distance / groupCoordinators.Count;

                if (tempAvg < closestDistance)
                {
                    closestDistance = tempAvg;
                    value = possibleCenter;
                }
            }

            return value;
        }

        /// <summary>
        /// Classe para calcular a distância entre dois pontos de uma imagem
        /// </summary>
        /// <param name="pointA">Ponto A</param>
        /// <param name="pointB">Ponto B</param>
        /// <param name="im">Imagem</param>
        /// <returns></returns>
        public static double CalculateDistance(Point pointA, Point pointB, Bitmap im)
        {
            if (im == null)
                throw new Exception("Imagem nula ao tentar calcular a distância");

            Color pixel = im.GetPixel(pointA.X, pointA.Y);
            Color centerPixel = im.GetPixel(pointB.X, pointB.Y);

            return Utils.CalculateDistance(pixel, centerPixel);
        }

        /// <summary>
        /// Método que calcula a distância entre dois pontos
        /// </summary>
        /// <param name="pixelA"></param>
        /// <param name="pixelB"></param>
        /// <returns></returns>
        public static double CalculateDistance(Color pixelA, Color pixelB)
        {
            double a = Convert.ToDouble(pixelA.R) - Convert.ToDouble(pixelB.R);
            double b = Convert.ToDouble(pixelA.G) - Convert.ToDouble(pixelB.G);
            double c = Convert.ToDouble(pixelA.B) - Convert.ToDouble(pixelB.B);

            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2) + Math.Pow(c, 2));
        }

        /// <summary>
        /// Método responsável por definir a nota de acordo com a distância do ponto para as extremidades
        /// </summary>
        /// <param name="pixel"></param>
        /// <param name="excludes"></param>
        /// <returns></returns>
        public static ENote GetColorNote(Color pixel, List<ENote> excludes = null)
        {
            ENote returnValue = ENote.Unknow;
            double closestDistance = double.MaxValue;
            double currentDistance;
            CubeMusicalization cube = CreateDefaultCube();

            cube.ColorNotes.ForEach(cn =>
            {
                currentDistance = CalculateDistance(cn.Color, pixel);

                if (currentDistance < closestDistance && (excludes == null || !excludes.Contains(cn.Note)))
                {
                    closestDistance = currentDistance;
                    returnValue = cn.Note;
                }
            });

            return returnValue;
        }

        /// <summary>
        /// Classe que cria um cube de musicalização default
        /// </summary>
        /// <returns></returns>
        public static CubeMusicalization CreateDefaultCube()
        {
            CubeMusicalization cube = new CubeMusicalization()
            {
                ColorNotes = new List<ColorNote>() 
                {
                    new ColorNote()
                    {
                        Color = Color.FromArgb(0, 0, 0),
                        Note = ENote.C
                    },
                    new ColorNote()
                    {
                        Color = Color.FromArgb(255, 0, 0),
                        Note = ENote.Dm
                    },
                    new ColorNote()
                    {
                        Color = Color.FromArgb(0, 255, 0),
                        Note = ENote.Em
                    },
                    new ColorNote()
                    {
                        Color = Color.FromArgb(255, 255, 0),
                        Note = ENote.F
                    },
                    new ColorNote()
                    {
                        Color = Color.FromArgb(0, 255, 255),
                        Note = ENote.G
                    },
                    new ColorNote()
                    {
                        Color = Color.FromArgb(255, 0, 255),
                        Note = ENote.Am
                    },
                    new ColorNote()
                    {
                        Color = Color.FromArgb(255, 255, 255),
                        Note = ENote.Si
                    }
                }
            };

            return cube;
        }

    }
}
