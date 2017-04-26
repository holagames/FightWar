using System;
using System.Text;
using System.Collections;
namespace Common
{
  

	public sealed class JSONTokener
	{
		    /** The input JSON. */
		    private string ins;
            public Hashtable nameValuePairs = new Hashtable();
		    private int pos;
	
		    public JSONTokener(string ins) {
		        // consume an optional byte order mark (BOM) if it exists
		     ///   if (ins != null && ins.StartsWith("\ufeff")) {
		     //       ins = ins.Substring(1);
		     //   }
		        this.ins = ins;
 		     	
		    }
		
		    public Object nextValue(){
		        int c = nextCleanInternal();
		        switch (c) {
		            case -1:
                       // error
                       // APaymentHelperDemo.toAndroidLog(tag, "nextValue -1 error");
                        return null;
		
		            case '{':
		                return readObject();
		
		            case '\'':
		            case '"':
		                return nextString((char) c);
		
		            default:
		                // pos--;
				        // APaymentHelperDemo.toAndroidLog(tag, "nextValue default:"+pos+" c:"+c);
		                return null;//readLiteral();
		        }
		    }
		
		    private int nextCleanInternal() {
       
		        while (pos < ins.Length) {
		            char c = (ins.Substring(pos++,1).ToCharArray())[0];
		            switch (c) {
		                case '\t':
		                case ' ':
		                case '\n':
		                case '\r':
		                    continue;
		
		                case '/':
                            if (pos == ins.Length)
                            {
		                        return c;
		                    }

                            char peek = (ins.Substring(pos,1).ToCharArray())[0];
		                    switch (peek) {
		                        case '*':
		                            // skip a /* c-style comment */
		                            pos++;
		                            int commentEnd = ins.IndexOf("*/", pos);
		                            if (commentEnd == -1) {
		                               // throw syntaxError("Unterminated comment");
                                       // error
                					   // APaymentHelperDemo.toAndroidLog(tag, "nextCleanInternal commentEnd == -1");
                                        return 0;
		                            }
		                            pos = commentEnd + 2;
		                            continue;
		
			                        case '/':
		                            // skip a // end-of-line comment
		                            pos++;
		                            skipToEndOfLine();
		                            continue;
		
		                        default:
		                            return c;
		                    }
		
		                case '#':
		                    /*
		                     * Skip a # hash end-of-line comment. The JSON RFC doesn't
		                     * specify this behavior, but it's required to parse
		                     * existing documents. See http://b/2571423.
		                     */
		                    skipToEndOfLine();
		                    continue;
		
		                default:
		                    return c;
		            }
		        }
		
		        return -1;
		    }
		
		    /**
		     * Advances the position until after the next newline character. If the line
		     * is terminated by "\r\n", the '\n' must be consumed as whitespace by the
		     * caller.
		     */
		    private void skipToEndOfLine() {
		        for (; pos < ins.Length; pos++) {
		            char c = ins.Substring(pos,1).ToCharArray()[0];
		            if (c == '\r' || c == '\n') {
		                pos++;
		                break;
		            }
		        }
		    }
		
		    /**
		     * Returns the string up to but not including {@code quote}, unescaping any
		     * character escape sequences encountered along the way. The opening quote
		     * should have already been read. This consumes the closing quote, but does
		     * not include it in the returned string.
		     *
		     * @param quote either ' or ".
		     * @throws NumberFormatException if any unicode escape sequences are
		     *     malformed.
		     */
		    public string nextString(char quote)  {
		        /*
		         * For strings that are free of escape sequences, we can just extract
		         * the result as a substring of the input. But if we encounter an escape
		         * sequence, we need to use a StringBuilder to compose the result.
		         */
		        StringBuilder builder = null;
		
		        /* the index of the first character not yet appended to the builder. */
		        int start = pos;
	
		        while (pos < ins.Length) {
		            char c = ins.Substring(pos++,1).ToCharArray()[0];
		            if (c == quote) { 
		                if (builder == null) {
		                    // a new string avoids leaking memory
						    string str = ins.Substring(start, pos - 1 - start);
						   // APaymentHelperDemo.toAndroidLog(tag, "start:"+start+" end:"+(pos - 1)+" nextString str1:"+str);
		                    return str;
		                } else {
		                    builder.Append(ins, start, pos - 1 - start);
						  //  APaymentHelperDemo.toAndroidLog(tag, "start:"+start+" end:"+(pos - 1)+" nextString str2:"+builder.ToString());
		                    return builder.ToString();
		                }
		            }
		
		            if (c == '\\') {
		                if (pos == ins.Length) {
		                   // throw syntaxError("Unterminated escape sequence");
						   // APaymentHelperDemo.toAndroidLog(tag, "nextString Unterminated escape sequence");
						    return "";
		                }
		                if (builder == null) {
		                   builder = new StringBuilder();
		                }
		                builder.Append(ins, start, pos - 1 - start);
		                builder.Append(readEscapeCharacter());
		                start = pos;					    
		            }
		        }
		
		        //throw syntaxError("Unterminated string");
               // APaymentHelperDemo.toAndroidLog(tag, "nextString Unterminated string");
                return "";
		    }
		
	
		    private char readEscapeCharacter() {
		        char escaped = ins.Substring(pos++,1).ToCharArray()[0];
//			    APaymentHelperDemo.toAndroidLog(tag, "readEscapeCharacter escaped:"+escaped);
		        switch (escaped) {
		            case 'u':
		                if (pos + 4 > ins.Length) {
		                   // throw syntaxError("Unterminated escape sequence");
                           
                            return ' ';
		                }
		                String hex = ins.Substring(pos, 4);
		                pos += 4;
                        return (char)Int16.Parse(hex);//Integer.parseInt(hex, 16);
		
		            case 't':
		                return '\t';
		
		            case 'b':
		                return '\b';
		
		            case 'n':
		                return '\n';
		
		            case 'r':
		                return '\r';
		
		            case 'f':
		                return '\f';
		
		            case '\'':
		            case '"':
		            case '\\':
		            default:
		                return escaped;
		        }
		    }
		

            private SFJSONObject readObject()
            {
                SFJSONObject result = new SFJSONObject();
		
		        /* Peek to see if this is the empty object. */
		        int first = nextCleanInternal();
		        if (first == '}') {
		            return null;
		        } else if (first != -1) {
		            pos--;
		        }
		
		        while (true) {
		            Object name = nextValue();
		            //APaymentHelperDemo.toAndroidLog(tag, "readObject name:" + name);
		            /*
		             * Expect the name/value separator to be either a colon ':', an
		             * equals sign '=', or an arrow "=>". The last two are bogus but we
		             * include them because that's what the original implementation did.
		             */
		            int separator = nextCleanInternal();
		            if (separator != ':' && separator != '=') {
		                //throw syntaxError("Expected ':' after " + name);
				//	    APaymentHelperDemo.toAndroidLog(tag, "Expected ':' after " + name);
						return null;
		            }
		            if (pos < ins.Length && ins.Substring(pos,1).ToCharArray()[0] == '>') {
		                pos++;
		            }
		            result.put((string) name, nextValue());
		
		            switch (nextCleanInternal()) {
		                case '}':
		                    return result;
		                case ';':
		                case ',':
		                    continue;
		                default:
                    //        APaymentHelperDemo.toAndroidLog(tag, "Unterminated object");
                            return null;
		                   // throw syntaxError("Unterminated object");
		            }
		        }
		    }
		
		

	}
}