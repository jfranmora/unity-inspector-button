using System;
using UnityEngine;

namespace JfranMora.Inspector
{
	[AttributeUsage(AttributeTargets.Method, Inherited = true)]
	public class ButtonAttribute : PropertyAttribute
	{
		public string Name { get; set; }
		public bool HideInPlayMode { get; set; }
		public bool HideInEditMode { get; set; }

		public ButtonAttribute() : this("")
		{
		}

		public ButtonAttribute(string name = "", bool hideInPlayMode = false, bool hideInEditMode = false)
		{
			this.Name = name;
			this.HideInPlayMode = hideInPlayMode;
			this.HideInEditMode = hideInEditMode;
		}
	}
}