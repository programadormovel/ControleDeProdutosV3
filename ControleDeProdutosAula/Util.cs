namespace ControleDeProdutosAula
{
	public class Util
	{
		public static byte[] ReadFully2(string input)
		{
			FileStream sourceFile = new FileStream(input, FileMode.Open); //Open streamer
			BinaryReader binReader = new BinaryReader(sourceFile);
			byte[] output = new byte[sourceFile.Length]; //create byte array of size file
			for (long i = 0; i < sourceFile.Length; i++)
				output[i] = binReader.ReadByte(); //read until done
			sourceFile.Close(); //dispose streamer
			binReader.Close(); //dispose reader
			return output;
		}

		internal static byte[]? ReadFully2(Func<string?> toString)
		{
			throw new NotImplementedException();
		}
	}
}
