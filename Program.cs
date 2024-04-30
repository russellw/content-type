using System;
using System.IO;
using MimeDetective;

class Program {
	// https://github.com/MediatedCommunications/Mime-Detective
	static readonly ContentInspector Inspector =
		new ContentInspectorBuilder() {
			Definitions =
				new MimeDetective.Definitions
					.ExhaustiveBuilder() { UsageType = MimeDetective.Definitions.Licensing.UsageType.PersonalNonCommercial }
					.Build()
		}
			.Build();

	static void Main(string[] args) {
		string path = ".";
		if (args.Length > 0)
			path = args[0];
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
		foreach (var Result in Results) {
			Console.Write('\t');
			Console.WriteLine(Result.Definition.File.Description);

			Console.Write("\t\t");
			Console.WriteLine(string.Join(", ", Result.Definition.File.Extensions));

			Console.Write("\t\t");
			Console.WriteLine(string.Join(", ", Result.Definition.File.Categories));

			Console.Write("\t\t");
			Console.WriteLine(Result.Definition.File.MimeType);
		}
	}
}
