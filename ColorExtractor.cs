using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FirstTask
{
    public static class ColorExtractor
    {
        public static Color[] GetDominantColors(BitmapSource source, int colorCount)
        {
            if (source == null || colorCount < 1)
                return new Color[] { Colors.Black };

            try
            {
                var formattedBitmap = new FormatConvertedBitmap(source, PixelFormats.Bgr32, null, 0);
                int stride = formattedBitmap.PixelWidth * 4;
                int size = formattedBitmap.PixelHeight * stride;
                byte[] pixels = new byte[size];
                formattedBitmap.CopyPixels(pixels, stride, 0);

                var sampledColors = new List<Color>();
                for (int i = 0; i < pixels.Length; i += 40)
                {
                    if (i + 2 < pixels.Length)
                    {
                        sampledColors.Add(Color.FromRgb(pixels[i + 2], pixels[i + 1], pixels[i]));
                    }
                }

                var colorClusters = ClusterColors(sampledColors, colorCount);

                var dominantColors = new List<Color>();
                foreach (var cluster in colorClusters)
                {
                    if (cluster.Count > 0)
                    {
                        int r = 0, g = 0, b = 0;
                        foreach (var color in cluster)
                        {
                            r += color.R;
                            g += color.G;
                            b += color.B;
                        }
                        dominantColors.Add(Color.FromRgb(
                            (byte)(r / cluster.Count),
                            (byte)(g / cluster.Count),
                            (byte)(b / cluster.Count)));
                    }
                }

                while (dominantColors.Count < colorCount)
                {
                    dominantColors.Add(Colors.Black);
                }

                return dominantColors.ToArray();
            }
            catch
            {
                return new Color[]
                {
                    Color.FromRgb(0x91, 0x3A, 0xC7),
                    Color.FromRgb(0x09, 0x09, 0x09),
                    Color.FromRgb(0x9B, 0xC2, 0xDB)
                };
            }
        }

        private static List<List<Color>> ClusterColors(List<Color> colors, int clusterCount)
        {
            var clusters = new List<List<Color>>();
            var centroids = new Color[clusterCount];

            Random rand = new Random();
            for (int i = 0; i < clusterCount; i++)
            {
                centroids[i] = colors[rand.Next(colors.Count)];
                clusters.Add(new List<Color>());
            }

            for (int iter = 0; iter < 5; iter++)
            {
                foreach (var cluster in clusters)
                {
                    cluster.Clear();
                }

                foreach (var color in colors)
                {
                    int closestCluster = 0;
                    double minDistance = double.MaxValue;

                    for (int i = 0; i < centroids.Length; i++)
                    {
                        double distance = ColorDistance(color, centroids[i]);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestCluster = i;
                        }
                    }

                    clusters[closestCluster].Add(color);
                }

                for (int i = 0; i < clusters.Count; i++)
                {
                    if (clusters[i].Count > 0)
                    {
                        int r = 0, g = 0, b = 0;
                        foreach (var color in clusters[i])
                        {
                            r += color.R;
                            g += color.G;
                            b += color.B;
                        }
                        centroids[i] = Color.FromRgb(
                            (byte)(r / clusters[i].Count),
                            (byte)(g / clusters[i].Count),
                            (byte)(b / clusters[i].Count));
                    }
                }
            }

            return clusters;
        }

        private static double ColorDistance(Color c1, Color c2)
        {
            return Math.Sqrt(
                Math.Pow(c1.R - c2.R, 2) +
                Math.Pow(c1.G - c2.G, 2) +
                Math.Pow(c1.B - c2.B, 2));
        }
    }
}