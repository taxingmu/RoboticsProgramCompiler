﻿using RoboticsProgramCompiler.Miscs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RoboticsProgramCompiler.Symbols.KUKA
{
    /// <summary>
    /// 设置参数
    /// </summary>
    public class SetPTPParams : Instruction, IParser
    {
        private const string regex = @"BAS \( #PTP_PARAMS , ([\S^\)^\s]*) \)";

        public override object[] Execute(params object[] arguments)
        {
            throw new System.NotImplementedException();
        }

        public Symbol[] Parse(Dictionary<string, object> arguments)
        {
            var text = arguments["text"] as string;
            var mc = Regex.Match(text, regex.Replace(" ", @"[\s]*"));
            if (!mc.Success) {
                return null;
            }

            var variable = new Variable() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["file"] as string),
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                Value = mc.Groups[1].Value
            };

            return new Symbol[] { new SetPTPParams() {
                Namespace = arguments["namespace"] as string,
                Name = UUID.Generate(arguments["file"] as string),
                File = arguments["file"] as string,
                Line = (int)arguments["line"],
                Column = (int)arguments["column"],
                symbols = new List<string>() { variable.Name }
            }, variable };
        }
    }
}