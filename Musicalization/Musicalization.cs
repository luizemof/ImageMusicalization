using Common;
using Common.Generation;
using Generation.KMeans;
using Generation.SURF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using WMPLib;
using System.Diagnostics;

namespace Musicalization
{
    public static class Musicalization
    {

        /// <summary>
        /// Gera a música, salva e toca.
        /// </summary>
        /// <param name="sequence">sequência que será executada</param>
        /// <param name="output">arquivo que será salvo</param>
        public static void GenerateAndPlay(List<IState> sequence, string output = "")
        {
            if (output == string.Empty)
                output = string.Concat(Environment.CurrentDirectory, @"\", "output.mp3");

            Generate(sequence, output);
            Play(output);
        }

        public static void Generate(List<IState> sequence, string output)
        {
            Converter<IState, string> converter = (a) =>
                {
                    if (a.ModelType == EModelType.KMeans)
                        return (((KMeansState)a).Element.SoundNote);
                    else if (a.ModelType == EModelType.SURF)
                        return (((SURFState)a).Element.SoundNote);
                    else
                        return string.Empty;
                };

            Generate(sequence.ConvertAll<string>(converter), output);
        }

        /// <summary>
        /// Gera o arquivo de um determinada sequência
        /// </summary>
        /// <param name="sequence">sequencia de arquivos que será gerada</param>
        /// <param name="outputFile">arquivo que será gerado</param>
        public static void Generate(IEnumerable<string> sequence, string outputFile)
        {
            byte[] buffer = new byte[1024];
            WaveFileWriter wavWriter = null;

            try
            {
                foreach (string file in sequence)
                {
                    if (file != string.Empty)
                    {
                        using (Mp3FileReader reader = new Mp3FileReader(file))
                        {
                            if (wavWriter == null)
                                wavWriter = new WaveFileWriter(outputFile, reader.Mp3WaveFormat);
                            
                            Mp3Frame frame;
                            while ((frame = reader.ReadNextFrame()) != null)
                                wavWriter.Write(frame.RawData, 0, frame.RawData.Length);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (wavWriter != null)
                    wavWriter.Dispose();
            }

        }

        /// <summary>
        /// Executa uma sequência.
        /// </summary>
        /// <param name="sequence"></param>
        public static void Play(List<IState> sequence)
        {
            sequence.ForEach(s =>
            {
                if(s.ModelType == EModelType.KMeans)
                    Play((((KMeansState)s).Element.SoundNote));
                else if (s.ModelType == EModelType.SURF)
                    Play((((SURFState)s).Element.SoundNote));
            });
        }

        /// <summary>
        /// Executa um arquivo.
        /// </summary>
        /// <param name="file"></param>
        public static void Play(string file)
        {
            using(Player player = new Player(file))
                player.Play();
        }
    }
}
