using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class TraverseDirectory
{
	struct TraversalStack
	{
		public string TopFolder;
		public int ChildIdxToGoOnReturn; // idx to go to when resuming from lower/inner level of depth
	}

	const int STACK_LIMIT = 500;
	const int FILE_LIMIT = 1000;

	public static IEnumerable<string> Do(string path)
	{
		Stack<TraversalStack> traversal = new Stack<TraversalStack>(5);

		TraversalStack initial;
		initial.TopFolder = path;
		initial.ChildIdxToGoOnReturn = 0;

		traversal.Push(initial);


		// guard against infinite loop
		int infiniteCounter = 0;

		TraversalStack currentStack;


		while (traversal.Count > 0)
		{
			++infiniteCounter;
			if (infiniteCounter > STACK_LIMIT)
			{
				break;
			}

			currentStack = traversal.Peek();
			//Debug.Log("in " + currentStack.TopFolder);

			bool toGoDeeper = false;

			string[] allInCurrentFolder = Directory.GetFileSystemEntries(currentStack.TopFolder);
			for (int n = currentStack.ChildIdxToGoOnReturn, len = allInCurrentFolder.Length; n < len; ++n)
			{
				++infiniteCounter;
				if (infiniteCounter > FILE_LIMIT)
				{
					break;
				}
				//Debug.Log("for loop: [" + n + "] " + allInCurrentFolder[n]);
				if (File.Exists(allInCurrentFolder[n]) && !allInCurrentFolder[n].EndsWith(".meta"))
				{
					yield return allInCurrentFolder[n].Replace("\\", "/");
				}
				else if (Directory.Exists(allInCurrentFolder[n]))
				{
					//Debug.Log("is folder: " + allInCurrentFolder[n]);
					TraversalStack deeper;
					deeper.TopFolder = allInCurrentFolder[n];
					deeper.ChildIdxToGoOnReturn = 0;



					currentStack.ChildIdxToGoOnReturn = n+1;
					traversal.Pop();
					traversal.Push(currentStack);



					traversal.Push(deeper);

					//Debug.Log("pushed " + allInCurrentFolder[n]);

					toGoDeeper = true;
					break;
				}
			}
			if (!toGoDeeper)
			{
				//Debug.Log("popped " + currentStack.TopFolder);
				traversal.Pop();
			}
		}
	}


#if UNITY_EDITOR
	//[MenuItem("Window/Test traverse folder")]
	public static void TestA()
	{
		//string folder = "C:/Users/Ferdi/Projects/_AssetStoreProducts/BuildReportTool/BuildReportToolU353/BuildReportUnityProject/Assets/BuildReport/Scripts";
		string folder = Application.dataPath;

		Debug.Log("traverse at: " + folder);
		foreach (string file in Do(folder))
		{
			Debug.Log("traverse stack: " + Path.GetFileName(file));
		}
	}
#endif
}
