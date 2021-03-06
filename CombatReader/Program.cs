﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CombatReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<EventLine>();
            //TextWriter tw = new TextWriter()
            using (TextReader tr = new StreamReader("..\\..\\ComLog1.txt"))
            {
                //int counter = 0;
                bool isEOF = false;
                do
                {
                    //counter++;
                    EventLine el = new EventLine();
                    string firstLine = tr.ReadLine();
                    if (firstLine != null)
                    {
                        el.TimeStamp = Convert.ToDateTime(firstLine.Split(']')[0].Remove(0, 1));
                        Console.WriteLine(el.TimeStamp);

                        el.Source.GetSource(el, firstLine);
                        Console.WriteLine(el.Source.Name);

                        el.Target.GetTarget(el, firstLine);
                        Console.WriteLine(el.Target.Name);

                        el.AbilityName.GetAbilityName(el, firstLine);
                        Console.WriteLine(el.AbilityName.Name + " " + el.AbilityName.ID);

                        el.Event.GetEvent(el, firstLine);
                        Console.WriteLine(el.Event.Name + " " + el.Event.ID);

                        el.Effect.GetEffectNameAndID(el, firstLine);
                        Console.WriteLine(el.Effect.Name + " " + el.Effect.ID);

                        el.Value.GetValue(el, firstLine);
                        Console.WriteLine(String.Format("{0} {1} {2}"
                            , el.Value.Amount, el.Value.Name, el.Value.ID));

                        el.Mitigation.GetMitigation(el, firstLine);
                        Console.WriteLine(el.Mitigation.Value + " " 
                            + el.Mitigation.Name + " " + el.Mitigation.ID);

                        bool isThreat = firstLine.Contains('<');
                        if (isThreat)
                        {
                            string preThreat = firstLine.Split('<')[1].Replace(">", "");
                            int eThreat;
                            bool isNumber2 = int.TryParse(preThreat, out eThreat);
                            el.Threat = eThreat;
                            Console.WriteLine(el.Threat);
                        }
                    }
                    else
                    {
                        isEOF = true;
                    }
                    list.Add(el);
                } while (isEOF == false);
                //Console.WriteLine(list.Count);
                Console.ReadLine();
                //3191
            }
        }
    }
}
