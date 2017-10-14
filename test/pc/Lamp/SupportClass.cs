using System;
public class SupportClass
{
	public static System.Object PutElement(System.Collections.Hashtable hashTable, System.Object key, System.Object newValue)
	{
		System.Object element = hashTable[key];
		hashTable[key] = newValue;
		return element;
	}

	/*******************************/
	public static void WriteStackTrace(System.Exception throwable, System.IO.TextWriter stream)
	{
		stream.Write(throwable.StackTrace);
		stream.Flush();
	}

}
