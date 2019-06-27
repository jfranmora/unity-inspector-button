using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace JfranMora.Inspector
{
	using Object = UnityEngine.Object;
	using Editor = UnityEditor.Editor;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Object), true, isFallback = true)]
	public class ButtonEditor : Editor
	{
		private InternalData internalData;

		private void OnEnable()
		{
			if (internalData == null) internalData = new InternalData(target.GetType());
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			// Draw buttons
			EditorGUILayout.Space();
			for (int i = 0; i < internalData.validMethods.Count; i++)
			{
				var currentButton = internalData.buttons[i];
				var method = internalData.validMethods[i];

				string buttonName = currentButton.Name.Length > 0 ? currentButton.Name : method.Name.SplitPascalCase();

				if (Application.isPlaying && !currentButton.HideInPlayMode || !Application.isPlaying && !currentButton.HideInEditMode)
				{
					if (GUILayout.Button(buttonName))
					{
						method.Invoke(target, null);
						EditorUtility.SetDirty(target);
					}
				}
			}
		}

		private class InternalData
		{
			public List<MethodInfo> validMethods;
			public List<ButtonAttribute> buttons;
			public Type objType;

			public InternalData(Type objType)
			{
				Initialize(objType);
			}

			public void Initialize(Type objType)
			{
				this.objType = objType;

				validMethods = new List<MethodInfo>();
				buttons = new List<ButtonAttribute>();

				foreach (var method in Utils.GetMethodsWithAttribute(objType, typeof(ButtonAttribute)))
				{
					if (method.ReturnType == typeof(void) && method.GetParameters().Length == 0)
					{
						validMethods.Add(method);
						buttons.Add(Utils.GetAttributeOfMethod<ButtonAttribute>(method));
					}
				}
			}
		}
	}

	static class Utils
	{
		public static IEnumerable<MethodInfo> GetMethodsWithAttribute(Type classType, Type attributeType)
		{
			return classType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
				.Where(methodInfo => methodInfo.GetCustomAttributes(attributeType, true).Length > 0);
		}

		public static T GetAttributeOfMethod<T>(MethodInfo methodInfo) where T : Attribute
		{
			return methodInfo.GetCustomAttributes(typeof(T), true).Cast<T>().FirstOrDefault();
		}
	}

	static class Extensions
	{
		public static string SplitPascalCase(this string input)
		{
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			stringBuilder.Append(char.ToUpper(input[0]));

			for (int i = 1; i < input.Length; i++)
			{
				char c = input[i];
				if (char.IsUpper(c) && !char.IsUpper(input[i - 1]))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(c);
			}

			return stringBuilder.ToString();
		}
	}
}