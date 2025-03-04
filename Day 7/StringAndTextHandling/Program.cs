class Char
{
  char c = 'A';
  char newLine = '\n';

  public static void CharMethods()
  {
    Console.WriteLine(char.ToUpper('c'));
    Console.WriteLine(char.ToString(' '));
    Console.WriteLine(char.ToString('c'));
    Console.WriteLine(char.IsWhiteSpace('\t'));
    Console.WriteLine(char.IsWhiteSpace(' '));
    Console.WriteLine(char.IsWhiteSpace('\n'));
  }

  public static void CultureInvariantMethods()
  {
    Console.WriteLine('İ');
    Console.WriteLine(char.ToLower('İ'));
    Console.WriteLine(char.ToLowerInvariant('İ'));

    Console.WriteLine('I');
    Console.WriteLine(char.ToLower('I'));
    Console.WriteLine(char.ToLowerInvariant('I'));
  }

  public static void CategorizingCharacters()
  {
    Console.WriteLine(char.IsPunctuation('.'));
    Console.WriteLine(char.IsPunctuation('!'));

    Console.WriteLine(char.GetUnicodeCategory('C'));
    Console.WriteLine(char.GetUnicodeCategory('c'));
    Console.WriteLine(char.GetUnicodeCategory('1'));
    Console.WriteLine(char.GetUnicodeCategory('X'));
    Console.WriteLine(char.GetUnicodeCategory('!'));
    Console.WriteLine(char.GetUnicodeCategory('İ'));
    Console.WriteLine(char.GetUnicodeCategory('例'));
    Console.WriteLine(char.GetUnicodeCategory('た'));
  }
}

class String
{
  public static void LiteralStrings()
  {
    string s1 = "Hello";
    string s2 = "First line\rSecond Line";
    Console.WriteLine(s1);
    Console.WriteLine(s2);
  }

  public static void StringConstructor()
  {
    Console.WriteLine(new string('w', 10));
  }

  public static void FromCharArray()
  {
    char[] ca = ['H', 'E', 'L', 'L', 'O'];
    Console.WriteLine(new string(ca));
  }

  public static void NullAndEmpty()
  {
    string empty = "";
    Console.WriteLine(empty == "");
    Console.WriteLine(empty == string.Empty);
    Console.WriteLine(empty.Length == 0);

    string nullString = null;
    Console.WriteLine(nullString == null);
    Console.WriteLine(nullString == "");

    string nullOrEmptyString = null;
    Console.WriteLine(string.IsNullOrEmpty(nullOrEmptyString));
  }

  public static void AccessingCharacters()
  {
    foreach (char c in "123")
    {
      Console.Write("| ");
      Console.Write(c + " | ");
    }
  }

  public static void SearchingWithinStrings()
  {
    Console.WriteLine("brown fox".EndsWith("Fox"));
    Console.WriteLine("brown fox".EndsWith("Fox".ToLowerInvariant()));
    Console.WriteLine("brown fox".Replace('o', 'a'));
    Console.WriteLine("brown fox".Replace('i', 'a'));
    Console.WriteLine("brown fox".Normalize());
  }

  public static void ManipulatingString()
  {
    string left2 = "12345".Substring(0, 2);
    string s1 = "helloworld".Remove(4);
    Console.WriteLine(left2);
    Console.WriteLine(s1);
  }
}

class Program
{
  static void Main()
  {
    // Char.CharMe
    // Char.CultureInvariantMe
    // Char.CategorizingChara
    // String.LiteralSt
    // String.StringConstr
    // String.FromChar
    // String.NullAndEmpty();
    // String.AccessingCharacters();
    // String.SearchingWithinStrings();
    String.ManipulatingString();
  }
}