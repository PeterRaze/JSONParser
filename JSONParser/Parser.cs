
using SpecialCharacter = JSONParser.Constants.SpecialCharacter;

namespace JSONParser
{
    public static class Parser
    {
        
        private static bool IsIndexValid(int tokens, int index)
        {
            return !(index >= tokens);
        }

        public static int Parse(List<object> tokens, int index)
        {
            var token = tokens[index];

            if (token == null)
            {
                throw new Exception($"Expecting {{ or [ Got null");
            }
            else if (token.GetType() == typeof(char)
                && (char)token == (char)SpecialCharacter.LEFT_BRACE)
            {
                return ParseObject(tokens, index + 1);
            }
            else if (token.GetType() == typeof(char)
                && (char)token == (char)SpecialCharacter.LEFT_BRACKET)
            {
                return ParseArray(tokens, index + 1);
            }
            else
            {
                throw new Exception($"Expecting {{ or [ Got {token}");
            }
        }

        private static int ParseArray(List<object> tokens, int index)
        {
            int result = index;
            if (!IsIndexValid(tokens.Count, index))
            {
                throw new Exception("Expecting ], Valid Type, { or [ Got EOF");
            }

            for (int i = index; i < tokens.Count; i++)
            {
                if (IsTypeAllowed(tokens[i]))
                {
                    i++;
                    if (!IsIndexValid(tokens.Count, i))
                    {
                        throw new Exception("Expecting ',', ], Got EOF");
                    }

                    if (tokens[i] == null)
                    {
                        throw new Exception($"Expecting ',', ] or Got null");
                    }
                    else if (tokens[i].GetType() == typeof(char)
                        && (char)tokens[i] == (char)SpecialCharacter.COMMA)
                    {
                        i++;
                        if (!IsIndexValid(tokens.Count, i))
                        {
                            throw new Exception("Expecting Valid Type, [ or { Got EOF");
                        }

                        if (tokens[i].GetType() == typeof(char)
                            && (char)tokens[i] == (char)SpecialCharacter.RIGHT_BRACKET)
                        {
                            throw new Exception("Expecting Valid Type, [ or { Got ]");
                        }
                        else
                        {
                            i--;
                            continue;
                        }
                    }
                    else if (tokens[i].GetType() == typeof(char)
                            && (char)tokens[i] == (char)SpecialCharacter.RIGHT_BRACKET)
                    {
                        return i + 1;
                    }
                    else
                    {
                        throw new Exception($"Expecting ',' or ] Got {tokens[i]}");
                    }
                }
                else if (tokens[i].GetType() == typeof(char)
                    && (char)tokens[i] == (char)SpecialCharacter.RIGHT_BRACKET)
                {
                    return i + 1;
                }
                else
                {
                    i = Parse(tokens, i);

                    if (!IsIndexValid(tokens.Count, i))
                    {
                        throw new Exception("Expecting ',' or ] Got EOF");
                    }

                    if (tokens[i] == null)
                    {
                        throw new Exception($"Expecting ',', ] or Got null");
                    }
                    else if (tokens[i].GetType() == typeof(char)
                        && (char)tokens[i] == (char)SpecialCharacter.COMMA)
                    {
                        i++;
                        if (!IsIndexValid(tokens.Count, i))
                        {
                            throw new Exception("Expecting Valid Type, [ or { Got EOF");
                        }

                        if (tokens[i].GetType() == typeof(char)
                            && (char)tokens[i] == (char)SpecialCharacter.RIGHT_BRACKET)
                        {
                            throw new Exception("Expecting Valid Type, [ or { Got ]");
                        }
                        else
                        {
                            i--;
                            continue;
                        }
                    }
                    else if (tokens[i].GetType() == typeof(char)
                            && (char)tokens[i] == (char)SpecialCharacter.RIGHT_BRACKET)
                    {
                        return i + 1;
                    }
                    else
                    {
                        throw new Exception($"Expecting ',' or ] or Got {tokens[i]}");
                    }
                }
            }

            return result + 1;
        }

        private static int ParseObject(List<object> tokens, int index)
        {
            int result = index;
            List<string> keys = new List<string>();
            if (!IsIndexValid(tokens.Count, index))
            {
                throw new Exception("Expecting String or } Got EOF");
            }

            for (int i = index; i < tokens.Count; i++)
            {
                if (tokens[i] == null)
                {
                    throw new Exception("Expecting String or } Got null");
                }
                else if (tokens[i].GetType() == typeof(char)
                    && (char)tokens[i] == (char)SpecialCharacter.RIGHT_BRACE)
                {
                    return i + 1;
                }
                else if (tokens[i].GetType() == typeof(string))
                {
                    if (keys.Contains((string)tokens[i]))
                    {
                        throw new Exception($"Duplicate Keys: {tokens[i]}");
                    }
                    else
                    {
                        keys.Add((string)tokens[i]);
                    }

                    i++;
                    if (!IsIndexValid(tokens.Count, i))
                    {
                        throw new Exception("Expecting : Got EOF");
                    }
                    
                    if (tokens[i] == null)
                    {
                        throw new Exception("Expecting : Got null");
                    }
                    else if (tokens[i].GetType() == typeof(char)
                        && (char)tokens[i] == (char)SpecialCharacter.COLON)
                    {
                        i++;
                        if (!IsIndexValid(tokens.Count, i))
                        {
                            throw new Exception("Expecting Valid Type, [ or } Got EOF");
                        }

                        if (IsTypeAllowed(tokens[i]))
                        {
                            i++;
                            if (!IsIndexValid(tokens.Count, i))
                            {
                                throw new Exception("Expecting ',' or } Got EOF");
                            }

                            if (tokens[i] == null)
                            {
                                throw new Exception("Expecting ',' or } Got null");
                            }
                            else if (tokens[i].GetType() == typeof(char)
                                && (char)tokens[i] == (char)SpecialCharacter.COMMA)
                            {
                                i++;
                                if (!IsIndexValid(tokens.Count, i))
                                {
                                    throw new Exception("Expecting String Got EOF");
                                }

                                if (tokens[i] == null)
                                {
                                    throw new Exception("Expecting String Got null");
                                }
                                else if (tokens[i].GetType() == typeof(string))
                                {
                                    i--;
                                    continue;
                                }
                                else
                                {
                                    throw new Exception($"Expecting String Got {tokens[i]}");
                                }
                            }
                            else if (tokens[i].GetType() == typeof(char)
                                && (char)tokens[i] == (char)SpecialCharacter.RIGHT_BRACE)
                            {
                                return i + 1;
                            }
                            else
                            {
                                throw new Exception($"Expecting ',' or }} Got {tokens[i]}");
                            }
                        }
                        else
                        {
                            i = Parse(tokens, i);

                            if (!IsIndexValid(tokens.Count, i))
                            {
                                throw new Exception("Expecting ',' or } Got EOF");
                            }

                            if (tokens[i] == null)
                            {
                                throw new Exception("Expecting ',' or } Got null");
                            }
                            else if(tokens[i].GetType() == typeof(char)
                                && (char)tokens[i] == (char)SpecialCharacter.RIGHT_BRACE)
                            {
                                return i + 1;
                            }
                            else if (tokens[i].GetType() == typeof(char)
                                && (char)tokens[i] == (char)SpecialCharacter.COMMA)
                            {
                                continue;
                            }
                            else
                            {
                                throw new Exception($"Expecting ',' or }} Got {tokens[i]}");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"Expecting : Got {tokens[i]}");
                    }
                }
                else
                {
                    throw new Exception($"Expecting String or }} Got {tokens[i]}");
                }
            }

            return result + 1;
        }

        private static bool IsTypeAllowed(object currentObject)
        {
            if (currentObject == null) return true;

            Type type = currentObject.GetType();

            if (type == typeof(string) || type == typeof(bool) ||
                type == typeof(double) || type == typeof(int))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
