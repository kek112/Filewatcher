   M S B A M L   `   `   `  ���� F  BFileWatcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:FileWatcherFileWatcher  =#clr-namespace:FileWatcher.ViewModelFileWatcher.ViewModel      FileWatcher.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     (myclr-namespace:FileWatcher     4conv#clr-namespace:FileWatcher.ViewModel     -     �Name$  FW�� � Title$ FileWatcher��$	��500��$	��700��$	��500��$	��700��.��]� .+��� $��Bottom=�.@��� .���� $��17��$��17��$��0,0,0,0q����� *ActiveImage+�� .���� ���� *	StatusBar+�  � RowDefinitions �� $
��Auto ���  � ColumnDefinitions �� $
��Auto ��� .+��� $��0��$��2��$
��Left=�.+���  �� Orientation$ 
Horizontal=�.+��� .+�A�  A� Columns$ 3��$
��Left=�.+��� $��Scan Folder��$��70��$
��Left=�$��5q����� *ScanFolderCommand+�� $��Start Watcher��$	��100��$
��Left=�$��5q����� *StartWatcherCommand+�� $��Stop Watcher��$	��100��$
��Left=�$��5q����� *StopWatcherCommand+A� $ 2��.+��� $	��200��$��5q����� *SourceFilePath+{��� *ElementActive+�� $	��...��$
��Left=�$��20��$��5q����� *SetSourceFilePathCommand+A� $ 2��.+��� $	��200��$��5q����� *TargetFilePath+{��� *ElementActive+�� $	��...��$
��Left=�$��20��$��5q����� *SetTargetFilePathCommand+A� $ 2��.+��� $	��200��$	��200�� �� SelectionMode$ Single=�$��5q�$��Visible=���. {��� *ElementActive+���� *Items+ �� SelectedIndex �� *SelectedListBoxItemIndex+���� .R��� $ 
Horizontal=�.+���  ��Name$ check��$��3q�$��Center=�	 d� 	IsChecked	 �� *	IsChecked+
 �� Mode$
 TwoWay=��� $��1q����� *Value+�� .+��� $��5q�$
��Left=�$	��Top=�$	��200������ *ExtraExtension+{��� *ElementActive+�� $��5q�$
��Left=�$	��Top=�$��Add Extension������ *AddExtraExtensionCommand+�� $��5q�$
��Left=�$	��Top=�$��Remove Selected Extension������ *RemoveExtensionCommand+A� $ 1��.+��� $ Vertical=�$��Right=�.+��� $��Informationen:��$��-5,0,0,0q�$
��Bold"��� ���� *InformationContent+�� ���� *
BottomList+