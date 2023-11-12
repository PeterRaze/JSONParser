namespace JSONParser
{
    public static class Lexer
    {
        public static List<object> Lex(string json)
        {
            List<object> tokens = new List<object>();

            for (int i = 0; i < json.Length; i++)
            {
                if (LexStr(json, ref tokens, ref i)) continue;
                if (LexNumber(json, ref tokens, ref i)) continue;
                if (LexBool(json, ref tokens, ref i)) continue;
                if (LexNull(json, ref tokens, ref i)) continue;
                if (Constants.WhiteSpaces.Contains(json[i])) continue;

                if (Constants.SpecialCharacters.Contains(json[i]))
                {
                    tokens.Add(json[i]);
                    continue;
                }

                throw new Exception($"Unexpected character {json[i]}");
            }

            return tokens;
        }

        private static bool LexStr(string json, ref List<object> tokens, ref int index)
        {
            if (json[index] == '"')
            {
                string token = "";

                for (int i = index + 1; i < json.Length; i++)
                {
                    if (json[i] != '"')
                    {
                        token += json[i];
                    }
                    else
                    {
                        tokens.Add(token);
                        index = i;
                        return true;
                    }
                }

                throw new Exception($"Expecting second \" to form a string");
            }
            else
            {
                return false;
            }
        }

        private static bool LexNumber(string json, ref List<object> tokens, ref int index)
        {
            string token = "";

            if (int.TryParse(json[index].ToString(), out int tokenInteger) || json[index] == '-')
            {
                if (json[index] == '-')
                {
                    token += "-";
                }
                else
                {
                    token += tokenInteger.ToString();
                }

                for (int i = index + 1; i < json.Length; i++)
                {
                    if (int.TryParse(json[i].ToString(), out tokenInteger))
                    {
                        token += tokenInteger.ToString();
                    }
                    else if (json[i] == '.')
                    {
                        int dotIndex = token.IndexOf('.');

                        if (token.Length == 1 && token[0] == '-')
                        {
                            throw new Exception($"'.' not allowed after '-'");
                        }
                        else if (dotIndex == -1 && token.Length > 0)
                        {
                            token += ".";
                        }
                        else
                        {
                            throw new Exception($"Second '.' not allowed");
                        }
                    }
                    else if (json[i] == '-')
                    {
                        throw new Exception("Unexpected '-'");
                    }
                    else
                    {
                        index = i - 1;
                        break;
                    }
                }

                if (token.Last() == '-') throw new Exception("Unexpected unattached '-'");
                if (token.Last() == '.') throw new Exception("Expecting number after '.'");

                if (token.Contains('.'))
                {
                    tokens.Add(double.Parse(token, System.Globalization.CultureInfo.InvariantCulture));
                }
                else
                {
                    tokens.Add(int.Parse(token));
                }

                return true;

            }
            else
            {
                return false;
            }
        }

        private static bool LexBool(string json, ref List<object> tokens, ref int index)
        {
            if (json[index] != 't' && json[index] != 'f') return false;
            if ((json.Length - index >= 4) && json.Substring(index, 4) == "true")
            {
                tokens.Add(true);
                index += 3;
                return true;
            }
            else if ((json.Length - index >= 5) && json.Substring(index, 5) == "false")
            {
                tokens.Add(false);
                index += 4;
                return true;
            }
            else
            {
                throw new Exception($"Expecting a valid type. Are you trying to insert a boolean or a string?!");
            }
        }
        private static bool LexNull(string json, ref List<object> tokens, ref int index)
        {
            if (json[index] != 'n') return false;

            if ((json.Length - index >= 4) && json.Substring(index, 4) == "null")
            {
                tokens.Add(null);
                index += 3;
                return true;
            }
            else
            {
                throw new Exception($"Expecting a valid type. Are you trying to insert a null or a string?!");
            }
        }
    }
}
