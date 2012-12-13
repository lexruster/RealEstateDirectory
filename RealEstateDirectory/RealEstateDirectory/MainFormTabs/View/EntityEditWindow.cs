using System;
using System.Windows;
using RealEstateDirectory.Interfaces;

namespace RealEstateDirectory.MainFormTabs.View
{
	public class EntityEditWindow : Window
	{
		private readonly Window _parent;
		private WindowState _oldParentState;

		public EntityEditWindow()
		{
			_parent = Application.Current.MainWindow;
			Owner = _parent;
			_oldParentState = _parent.WindowState;
			WindowStyle = WindowStyle.ThreeDBorderWindow;
			ResizeMode = ResizeMode.CanMinimize;
			StateChanged += EntityEditWindow_StateChanged;
		}

		private void EntityEditWindow_StateChanged(object sender, EventArgs e)
		{
			switch (this.WindowState)
			{
				case WindowState.Maximized:
				case WindowState.Normal:
					_parent.WindowState = _oldParentState;
					break;
				case WindowState.Minimized:
					_oldParentState = _parent.WindowState;
					_parent.WindowState = WindowState.Minimized;
					break;
			}
		}
	}
}