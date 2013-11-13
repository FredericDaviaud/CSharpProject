using FFR.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;

namespace FFR.Parser
{
    public class SongParser
    {
        public String SongTitle { get; private set; }
        public String SongArtist { get; private set; }
        public float SongOffset { get; private set; }
        public List<Arrow> ArrowList { get; set; }
        
        private Mp3FileReader reader;
        private String arrowColor;
        TimeSpan duration;


        public List<Arrow> parse(String filePath)
        {
            string text;
            int lineNumber = 0;
            int mesureId = 1;
            int mesureSize = 0;
            List<Mesure> mesureList;

            StreamReader streamReader = new StreamReader(filePath);

            mesureList = new List<Mesure>();
            ArrowList = new List<Arrow>();

            while ((text = streamReader.ReadLine()) != null)
            {
                lineNumber++;
                if (lineNumber > 27)
                {
                    mesureSize++;
                    if (text.Substring(0, 1).Equals(",") || text.Substring(0, 1).Equals(";"))
                    {
                        mesureList.Add(new Mesure(mesureId, mesureSize - 1));
                        mesureSize = 0;
                        mesureId++;
                    }
                }
                else if (lineNumber == 1 || lineNumber == 3 || lineNumber == 13)
                {
                    switch (lineNumber)
                    {
                        case 1:
                            SongTitle = text.Substring(7, text.Length - 8);
                            break;
                        case 3:
                            SongArtist = text.Substring(8, text.Length - 9);
                            break;
                        case 13:
                            SongOffset = float.Parse((text.Substring(8, text.Length - 9)).Replace(".", ","));
                            break;
                        default:
                            break;
                    }
                }
            }

            streamReader = new StreamReader(filePath);
            mesureId = 1;
            float time = 0;
            reader = new Mp3FileReader("Songs\\Almost There.mp3");
            duration = reader.TotalTime;
            for (int i = 0; i < 27; i++) streamReader.ReadLine();
            
            do
            {
                for (int i = 0; i < mesureList[mesureId].size; i++)
                {
                    text = streamReader.ReadLine();
                    if (text.Substring(0, 1).Equals("1") || text.Substring(0, 1).Equals("2")) ArrowList.Add(new Arrow(getArrowColor(mesureList[mesureId], i + 1), Rows.Row1, time));
                    if (text.Substring(1, 1).Equals("1") || text.Substring(1, 1).Equals("2")) ArrowList.Add(new Arrow(getArrowColor(mesureList[mesureId], i + 1), Rows.Row2, time));
                    if (text.Substring(2, 1).Equals("1") || text.Substring(2, 1).Equals("2")) ArrowList.Add(new Arrow(getArrowColor(mesureList[mesureId], i + 1), Rows.Row3, time));
                    if (text.Substring(3, 1).Equals("1") || text.Substring(3, 1).Equals("2")) ArrowList.Add(new Arrow(getArrowColor(mesureList[mesureId], i + 1), Rows.Row4, time));
                    time += ((float) duration.TotalMilliseconds / 1000) / mesureList.Count / mesureList[mesureId].size;
                }
                streamReader.ReadLine();
                mesureId++;
            } while (mesureId != mesureList.Count);

            return ArrowList;
        }

        private String getArrowColor(Mesure mesure, int note)
        {
            if (note > (mesure.size * 3 / 4)) note -= (mesure.size * 3 / 4);
            else if (note > (mesure.size / 2)) note -= mesure.size / 2;
            else if (note > (mesure.size / 4)) note -= mesure.size / 4;

            if (note == 1) return ArrowColors.Red;
            if (mesure.size / 8 + 1 == note) return ArrowColors.Blue;
            switch (mesure.size)
            {
                case 16: arrowColor = ArrowColors.Yellow;
                    break;
                case 12: arrowColor = ArrowColors.Purple;
                    break;
                case 24: arrowColor = get24thColor(note);
                    break;
                case 32: arrowColor = get32thColor(note);
                    break;
                case 48: arrowColor = get48thColor(note);
                    break;
                case 64: arrowColor = get64thColor(note);
                    break;
                case 96: arrowColor = get96thColor(note);
                    break;
                case 192: arrowColor = get192thColor(note);
                    break;
            }
            return arrowColor;
        }

        private String get24thColor(int note)
        {
            if (note == 2 || note == 6) return ArrowColors.Pink;
            else return ArrowColors.Purple;
        }

        private String get32thColor(int note)
        {
            if (note == 3 || note == 7) return ArrowColors.Yellow;
            else return ArrowColors.Orange;
        }

        private String get48thColor(int note)
        {
            if (note == 3 || note == 11) return ArrowColors.Pink;
            else if (note == 4 || note == 10) return ArrowColors.Yellow;
            else if (note == 5 || note == 9) return ArrowColors.Purple;
            else return ArrowColors.Cyan;
        }

        private String get64thColor(int note)
        {
            if (note == 5 || note == 13) return ArrowColors.Yellow;
            else if (note == 4 || note == 8 || note == 12 || note == 16) return ArrowColors.Orange;
            else return ArrowColors.Green;
        }

        private String get96thColor(int note)
        {
            if (note == 3 || note == 11) return ArrowColors.Yellow;
            else if (note == 5 || note == 21) return ArrowColors.Pink;
            else if (note == 9 || note == 17) return ArrowColors.Purple;
            else if (note == 3 || note == 11 || note == 15 || note == 23) return ArrowColors.Cyan;
            else return ArrowColors.White;
        }

        private String get192thColor(int note)
        {
            if (note == 13 || note == 37) return ArrowColors.Yellow;
            else if (note == 9 || note == 41) return ArrowColors.Pink;
            else if (note == 17 || note == 33) return ArrowColors.Purple;
            else if (note == 5 || note == 21 || note == 29 || note == 45) return ArrowColors.Cyan;
            else return ArrowColors.White;
        }
    }
}
