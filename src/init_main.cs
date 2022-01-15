using System;
using System.Collections.Generic;
using System.Linq;
using IniParser;
using IniParser.Model;

namespace init {
    class main {
        private static void Main(string[] args)
        {
            //init program
            if (args.Length == 0) {
                Console.WriteLine("Error>>>缺少参数");
                return;
            }
            else {
                foreach (var args_item in args) {
                    Execute(args_item);
                }
            }
        }

        public static void Execute(string inParameter)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("D:\\System Software\\CMDitem\\config\\init.ini");
            //set search result
            bool bool_MatchItemSuccessfully = false;
            //Do the game:
            foreach (var iSectionName in data.Sections) {
                //get AlterName string
                string tmp_str_sectionAlterName = data[iSectionName.SectionName]["AlterName"];
                if (tmp_str_sectionAlterName == null) {
                    Console.WriteLine("Error>>>name 'AlterName' is empty. Please check file init.ini. Program exit");
                    Environment.Exit(-1);
                }
                //split [string]AlterName into List<string>AlterName
                List<string> tmp_list_sectionAlterName = CutString(tmp_str_sectionAlterName, ',');
                foreach (string part in tmp_list_sectionAlterName) {
                    if (part.Equals(inParameter)) {
                        //get ExecuteCmd string
                        string str_sectionCMD = data[iSectionName.SectionName]["ExecuteCmd"];
                        if (str_sectionCMD == null) {
                            Console.WriteLine("Error>>>name 'ExecuteCmd' is empty. Please check file init.ini");
                        }
                        //split [string]ExecuteCmd into List<string>ExecuteCmd
                        List<string> list_sectionCMD = CutString(str_sectionCMD, ',');
                        foreach (string Cmd_name_item in list_sectionCMD) {
                            // get value(the things you are going to execute in shell) by name indexing
                            string tmp_cmd_value = data[iSectionName.SectionName][Cmd_name_item];
                            // if the value is not null and then execute the command
                            if (tmp_cmd_value != null) {
                                if (tmp_cmd_value.Equals("") == false) {
                                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                                    System.Diagnostics.ProcessStartInfo startInfo =
                                        new System.Diagnostics.ProcessStartInfo();
                                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                    startInfo.FileName = "cmd.exe";
                                    startInfo.Arguments = "/C " + tmp_cmd_value;
                                    process.StartInfo = startInfo;
                                    process.Start();
                                }
                            }
                            else {
                                Console.WriteLine("Error>>>value <" + Cmd_name_item +
                                                  "> Do not associate to name. Please check file init.ini");
                            }
                        }
                        bool_MatchItemSuccessfully = true;
                        break;
                    }
                }
                if (bool_MatchItemSuccessfully) {
                    break;
                }
            }
            if (!bool_MatchItemSuccessfully) {
                Console.WriteLine("ERROR>>>No section found, search cmd keyword<" + inParameter +
                                  "> failed, please check your init.ini file or the parameter entered");
            }
        }


        public static List<string> CutString(String SourceString, char SplitTarget)
        {
            List<string> tmp_list_SourceString = new List<string>();
            try {
                string[] temp = SourceString.Split(SplitTarget);
                tmp_list_SourceString = temp.ToList();
            }
            catch (NullReferenceException) {
                tmp_list_SourceString.Add(SourceString[0].ToString());
            }
            return tmp_list_SourceString;
        }
    }
}