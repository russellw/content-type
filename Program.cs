using System;
using System.IO;
using MimeDetective;

class Program {
	// https://github.com/MediatedCommunications/Mime-Detective
	static readonly ContentInspector Inspector =
		new ContentInspectorBuilder() { Definitions = MimeDetective.Definitions.Default.All() }.Build();

	static void Main(string[] args) {
		if (args.Length == 0) {
			Console.WriteLine("Please provide a directory or file path as an argument.");
			return;
		}

		string path = args[0];
		if (File.Exists(path)) {
			// It's a file
			PrintMimeType(path);
		} else if (Directory.Exists(path)) {
			// It's a directory
			ListFiles(path);
		} else {
			Console.WriteLine("The specified path does not exist.");
		}
	}

	static void ListFiles(string directoryPath) {
		foreach (string filePath in Directory.GetFiles(directoryPath)) {
			PrintMimeType(filePath);
		}

		foreach (string subDir in Directory.GetDirectories(directoryPath)) {
			ListFiles(subDir);
		}
	}

	static void PrintMimeType(string filePath) {
		Console.WriteLine(filePath);
		var Results = Inspector.Inspect(filePath);
		Console.WriteLine(Results.ToString());
	}
}
