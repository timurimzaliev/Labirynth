using System;
using System.IO;

namespace Labirynth
{
    class Program
    {
        static int[] position = { 0, 1 };
        static int rout = 1;
        static int[] extremeWidth = { 0, 0 };
        static int[] extremeHeight = { 0, 0 };

        static void Main(string[] args)
        {
            int count;

            using (StreamReader streamReader = File.OpenText("../../large-test.in.txt"))
            {
                count = Convert.ToInt32(streamReader.ReadLine());
                for (int i = 0; i < count; i++)
                {
                    //Решение задания
                    Console.WriteLine("Case #" + (i + 1));
                    WriteToTxt("Case #" + (i + 1));
                    Case(streamReader.ReadLine());
                }
            }
            Console.ReadKey();
            WriteToTxt("");
        }

        static void Case(string road)
        {
            char[] roadEnter = road.Substring(0, road.IndexOf(' ')).ToCharArray();
            char[] roadExit = road[(road.IndexOf(' ') + 1)..].ToCharArray();

            //Старт из входа
            for (int i = 0; i < roadEnter.Length; i++)
            {
                StepOfRoad(roadEnter[i]);
            }
            EndRoad();

            //Старт из выхода
            for (int i = 0; i < roadExit.Length; i++)
            {
                StepOfRoad(roadExit[i]);
            }
            EndRoad();

            bool[,,] rooms = new bool[extremeWidth[1] - extremeWidth[0] + 1, extremeHeight[1] - extremeHeight[0] + 1, 4];

            //Старт из входа
            for (int i = 0; i < roadEnter.Length; i++)
            {
                StepOfRoad(roadEnter[i]);
                //Проставление значений true для каждой комнаты в тех сторонах, где нет стен
                if (roadEnter[i] == 'W')
                {
                    if (rout == 0)
                    {
                        try
                        {
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0], 1] = true;
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0] - 1, 0] = true;
                        }
                        catch (Exception)
                        {
                        }

                    }
                    if (rout == 1)
                    {
                        try
                        {
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0], 0] = true;
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0] + 1, 1] = true;
                        }
                        catch (Exception)
                        {
                        }

                    }
                    if (rout == 2)
                    {
                        try
                        {
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0], 3] = true;
                            rooms[position[0] - extremeWidth[0] + 1, position[1] - extremeHeight[0], 2] = true;
                        }
                        catch (Exception)
                        {
                        }

                    }
                    if (rout == 3)
                    {
                        try
                        {
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0], 2] = true;
                            rooms[position[0] - extremeWidth[0] - 1, position[1] - extremeHeight[0], 3] = true;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            EndRoad();

            //Старт из выхода
            for (int i = 0; i < roadExit.Length; i++)
            {
                StepOfRoad(roadExit[i]);
                //Проставление значений true для каждой комнаты в тех сторонах, где нет стен
                if (roadExit[i] == 'W')
                {
                    if (rout == 0)
                    {
                        try
                        {
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0], 1] = true;
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0] - 1, 0] = true;
                        }
                        catch (Exception)
                        {
                        }

                    }
                    if (rout == 1)
                    {
                        try
                        {
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0], 0] = true;
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0] + 1, 1] = true;
                        }
                        catch (Exception)
                        {
                        }

                    }
                    if (rout == 2)
                    {
                        try
                        {
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0], 3] = true;
                            rooms[position[0] - extremeWidth[0] + 1, position[1] - extremeHeight[0], 2] = true;
                        }
                        catch (Exception)
                        {
                        }

                    }
                    if (rout == 3)
                    {
                        try
                        {
                            rooms[position[0] - extremeWidth[0], position[1] - extremeHeight[0], 2] = true;
                            rooms[position[0] - extremeWidth[0] - 1, position[1] - extremeHeight[0], 3] = true;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            EndRoad();

            //Ответ
            for (int i = extremeHeight[1] - extremeHeight[0]; i >= 0; i--)
            {
                string strLine = "";
                for (int j = 0; j < extremeWidth[1] - extremeWidth[0] + 1; j++)
                {
                    strLine += BoolToHex(rooms[j, i, 0], rooms[j, i, 1], rooms[j, i, 2], rooms[j, i, 3]);
                }
                Console.WriteLine(strLine);
                WriteToTxt(strLine);
            }

            Nullify();
        }

        static void StepOfRoad(char r)
        {
            if (r == 'W')
            {
                //Определение положения наблюдателя и размеров лаберинта
                DefinePosition();
            }
            else if (r == 'R')
            {
                //Определение направления наблюдателя при повороте налево L
                DefineRotation_R();
            }
            else
            {
                //Определение направления наблюдателя при повороте направо R
                DefineRotation_L();
            }
        }

        static void DefinePosition()
        {
            switch (rout)
            {
                case 0:
                    {
                        position[1]++;
                        if (position[1] > extremeHeight[1])
                            extremeHeight[1]++;
                        break;
                    }
                case 1:
                    {
                        position[1]--;
                        if (position[1] < extremeHeight[0])
                            extremeHeight[0]--;
                        break;
                    }
                case 2:
                    {
                        position[0]--;
                        if (position[0] < extremeWidth[0])
                            extremeWidth[0]--;
                        break;
                    }
                case 3:
                    {
                        position[0]++;
                        if (position[0] > extremeWidth[1])
                            extremeWidth[1]++;
                        break;
                    }
            }
        }

        static void DefineRotation_R()
        {
            switch (rout)
            {
                case 0:
                    {
                        rout = 3;
                        break;
                    }
                case 1:
                    {
                        rout = 2;
                        break;
                    }
                case 2:
                    {
                        rout = 0;
                        break;
                    }
                case 3:
                    {
                        rout = 1;
                        break;
                    }
            }
        }

        static void DefineRotation_L()
        {
            switch (rout)
            {
                case 0:
                    {
                        rout = 2;
                        break;
                    }
                case 1:
                    {
                        rout = 3;
                        break;
                    }
                case 2:
                    {
                        rout = 1;
                        break;
                    }
                case 3:
                    {
                        rout = 0;
                        break;
                    }
            }
        }

        static void EndRoad()
        {
            if (rout == 0)
            {
                rout = 1;
                extremeHeight[1]--;
            }
            else if (rout == 1)
            {
                rout = 0;
                extremeHeight[0]++;
            }
            else if (rout == 2)
            {
                rout = 3;
                extremeWidth[0]++;
            }
            else
            {
                rout = 2;
                extremeWidth[1]--;
            }
        }

        static string BoolToHex(bool north, bool south, bool east, bool west)
        {
            int hex = 0;

            if (west)
                hex += 8;
            if (east)
                hex += 4;
            if (south)
                hex += 2;
            if (north)
                hex += 1;

            if (hex < 10)
                return Convert.ToString(hex);
            else if (hex == 10)
                return "a";
            else if (hex == 11)
                return "b";
            else if (hex == 12)
                return "c";
            else if (hex == 13)
                return "d";
            else if (hex == 14)
                return "e";
            else return "f";
        }

        static void Nullify()
        {
            position[0] = 0;
            position[1] = 1;
            rout = 1;
            extremeWidth[0] = 0;
            extremeWidth[1] = 0;
            extremeHeight[0] = 0;
            extremeHeight[1] = 0;

        }

        static void WriteToTxt(string str)
        {
            using FileStream file = new FileStream("../../Answer.txt", FileMode.Append);
            using StreamWriter stream = new StreamWriter(file);
            stream.WriteLine(str);
        }
    }
}

