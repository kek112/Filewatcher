   M S B A M L   `   `   `  ����F  BFileWatcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:FileWatcherFileWatcher  5   	   =#clr-namespace:FileWatcher.ViewModelFileWatcher.ViewModel  5   	       FileWatcher.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   (myclr-namespace:FileWatcher     5   	   4conv#clr-namespace:FileWatcher.ViewModel     5   	   -   5          �Name$  FW��5   	    � Title$ FileWatcher��5   	   $	��500��5	   	   $	��700��5
   	   $	��500��5   	   $	��700��5   	   .��5      ]� .+�5   
   �� $��Bottom=�6   .@�5      �� .��5      �� $��17��67   $��17��6B   $��0,0,0,0q�6N   ��6   �� *ActiveImage+6N   5      �� 5      .��5      �� ��6   �� *	StatusBar+5      5      � 5   
    � RowDefinitions 5       �� 5!      $
��Auto �6    �� 5"       � ColumnDefinitions 5%      �� 5&      $
��Auto �6#   �� 5'      .+�5+      �� $��0��5-      $��2��5.      $
��Left=�5/      .+�52      ��  �� Orientation$ 
Horizontal=�6   .+�53      �� .+�54      A�  A� Columns$ 3��6&   $
��Left=�62   .+�55      �� $��Scan Folder��6K   $��70��6a   $
��Left=�6l   $��5q�6�   ��6%   �� *ScanFolderCommand+6�   �� 56      $��Start Watcher��6M   $	��100��6e   $
��Left=�6q   $��5q�6�   ��6%   �� *StartWatcherCommand+6�   �� 57      $��Stop Watcher��6L   $	��100��6c   $
��Left=�6o   $��5q�6�   ��6%   �� *StopWatcherCommand+6�   58      A� 5:      $ 2��6&   .+�5<      �� $	��200��6F   $��5q�6R   ��6&   �� *SourceFilePath+{�6]   �� *ElementActive+6�   �� 5=      $	��...��6%   $
��Left=�6`   $��20��6{   $��5q�6�   ��63   �� *SetSourceFilePathCommand+6�   5?      A� 5B      $ 2��6&   .+�5D      �� $	��200��6F   $��5q�6R   ��6&   �� *TargetFilePath+{�6]   �� *ElementActive+6�   �� 5E      $	��...��6%   $
��Left=�6`   $��20��6{   $��5q�6�   ��63   �� *SetTargetFilePathCommand+6�   5G      A� 5H      $ 2��6&   .+�5I      �� $	��200��5K      $	��200��5L   !    �� SelectionMode$ Single=�5O   !   $��5q�5P      $��Visible=�5R      ��. {�5J   !   �� *ElementActive+��5M      �� *Items+ �� SelectedIndex 5N   !   �� *SelectedListBoxItemIndex+��5T   &   �� 5U   *   .R�5V   .   �� $ 
Horizontal=�69   .+�5W   2   ��  ��Name$ check��6;   $��3q�6w   $��Center=�6�   	 d� 	IsChecked	 6J   �� *	IsChecked+
 �� Mode$
 TwoWay=�6�   �� 5X   2   $��1q�6]   ��6C   �� *Value+6]   5Y   /   5Z   +   5\      �� 5]      .+�5_   "   �� $��5q�6*   $
��Left=�65   $	��Top=�6P   $	��200��6h   ��6t   �� *ExtraExtension+{�6�   �� *ElementActive+�� 5`   "   $��5q�6)   $
��Left=�64   $	��Top=�6O   $��Add Extension��6g   ��6   �� *AddExtraExtensionCommand+�� 5a   "   $��5q�6)   $
��Left=�64   $	��Top=�6O   $��Remove Selected Extension��6g   ��6�   �� *RemoveExtensionCommand+5b      5c      5f      A� 5h      $ 1��6"   .+�5i      �� $ Vertical=�6%   $��Right=�6<   .+�5j      �� $��Informationen:��6$   $��-5,0,0,0q�6=   $
��Bold"�6O   6d   �� 5k      ��6(   �� *InformationContent+6N   5l      5n      5p      �� 5s      ��6   �� *
BottomList+5u      5w      5x      5y      5|      