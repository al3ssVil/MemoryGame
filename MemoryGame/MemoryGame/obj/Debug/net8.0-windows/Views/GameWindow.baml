   M S B A M L   `   `   `  ����E  AMemoryGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MemoryGame.ViewsMemoryGame.Views  5   	   =#clr-namespace:MemoryGame.ViewModelsMemoryGame.ViewModels  5   	   !    MemoryGame.Views.GameWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:MemoryGame.Views     5   	   2vm#clr-namespace:MemoryGame.ViewModels     5   	     � WindowStartupLocation$  CenterScreen=�5	   	    � Title$ 
MemoryGame��5
   	   $	��700��6   $	��940��6)    � DataContext 5      )  #MemoryGame.ViewModels.GameViewModel  5   
    � 	Resources 5         BoolToVisibilityConverter �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Padding
 �� Triggers P� Property P� Value��%j  &	        '��      '�� �    �� 5   
   6'   �� 5   
   " ��6   .�5      ��  � ��$ Segoe Script)�6+   �� 5       � �� �	#FF34495E6+   �� 5       � ��
 �����6+   �� 5       � ��$ 5q�6'   �� 5       � ��$	 8 4q�6(   �� 5       � ��$ 14(�6)   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 0q�60   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 10��5   !   #	 z��5    !   ._�5!      �� $��Center=�6/   $��Center=�6L   5"      5#      5%      
 5&      P� 5'       �@J�IsMouseOver$
 True��61   . �5(      ��  � �� �	#FF1ABC9C63   5)      5+      �� 5-   
   " ��6   .�5.      ��  � ��$ Segoe UI)�6+   �� 5/       � ��
 �����6+   �� 50       � ��$ 12(�6)   51      52      .��54      �  � ColumnDefinitions 55   
   �� 56      $	��200 �6   �� 57      $��* �6   .+�5:   
   � $��0��6   .+�5;      �� $%��../Images/SidebarBackground.jpg��6   $
��Fill=�5<       J� Opacity$ 1Q�5=       K� ZIndex$ 0��5>      �� 5@      ���	#802C3E50$ 1��60   .+�5A      �� $
��Menu��6   $��Segoe Script)�6(   $��22(�6B   
�������$��0 0 0 10q�6c   $��Center=�6u   �� 5C      $
��File��6   
�������$
��Bold"�6;   $��Segoe Script)�6N   $��16(�6h   �� 5D      $��0 5 0 10q�6h   $��Segoe Script)�6z   ��6   �� *
Categories+ �� SelectedItem 6>   �� *SelectedCategory+{�6�   �� *IsGameInactive+�� 5E      $��🎮New Game��6   $
��0 10q�6Q   ��6.   �� *NewGameCommand+{�6_   �� *IsGameInactive+�� 5F      $��📂Open Game��6   $
��0 10q�6S   ��6/   �� *OpenGameCommand+{�6a   �� *IsGameInactive+�� 5G      $��💾 Save Game��6   $
��0 10q�6T   ��60   �� *SaveGameCommand+6T   �� 5H      $��📊Statistics��6   $
��0 10q�6Z   ��60   �� *ShowStatisticsCommand+6Z   �� 5I      $��🚪Exit��6   $��	0 20 0 10q�6J   
���  ��
���������6*   �� *ExitCommand+6r   �� 5K      $��Options��6   $��Segoe Script)�6+   
�������$
��Bold"�6X   �� 5L      $��Standard (4x4)��6   $	��0 5q�6b   ��62   �� *SetStandardBoardSizeCommand+{�6o   �� *IsGameInactive+�� 5N       �� Orientation$ 
Horizontal=�6   $	��0 5q�66   .+�5O      �� $��M:��6    $��Segoe Script)�6*   
�������$��Center=�6W   $��0 0 5 0q�6r   �� 5P      $��40��6   $��Segoe Script)�6*    �� DisplayMemberPath$ .��5R      ��5P   D   �� *
RowOptions+ 5Q      �� *SelectedRows+{�5R   5   �� *IsGameInactive+�� 5T      $��N:��6    $��Segoe Script)�6*   
�������$��Center=�6W   $��10 0 5 0q�6r   �� 5U      $��40��6   $��Segoe Script)�6*   $ .��5W      ��5U   D   �� *ColumnOptions+ 5V      �� *SelectedColumns+{�5W   5   �� *IsGameInactive+5X      �� 5Y      $#��Customizable Timert(seconds):��6   
�������$��Center=�6T   $	��0 5q�6o   $��Segoe Script)�6|   �� 5Z      $��50��6   $��Center=�6B   $��Center=�6]   $��5q�6z   $��Segoe Script)�6�   ��6%   �� *CustomTimer+{�6�   �� *IsGameInactive+�� 5[      $��Apply Custom Size��6   $
��0 10q�6c   ��65   �� *SetCustomBoardSizeCommand+{�6q   �� *IsGameInactive+�� 5]      $
��Help��6   $��Segoe Script)�6(   
�������$
��Bold"�6U   $��0 10 0 0q�6g   �� 5^      $��❓About��6   ��6*   �� *ShowAboutCommand+5_      5`      � 5b   
   $��1��6   .+�5c      �� $"��../Images/BackgroundGame.jpg��6   $
��Fill=�5d      $	 0.8Q�5e      $ 0��5f      �� 5h      $��24(�5i      $
��Bold"�5j      
�������$��Center=�5l      $	��Top=�5m      $��0,10,0,0q�5n      $��Segoe Script)�5o      ��5h      �� *ElapsedTime+5o      � 5q      $ 1��6   $��0,50,0,0q�6$   .+�5r      �� $��Welcome to Memory Game��6   $��30(�5s      $��SemiBold"�5t      
���   �$��Center=�5v      $��Center=�5w      $��Segoe Script)�5x      $��	0,-50,0,0q�5y      y�5z      �� *WelcomeTextVisibility+ �� 	Converter# [  � 5|      -   5         �Name$ GameBoardGrid��5|      $��Stretch=�6.   $��Stretch=�6L   $	��600��6h   $	��600��6t   �� 5}      ��6   �� *	GameCards+��5~      �� 5      .R�5�      A�  A� Columns 6*   �� *SelectedColumns+ A� Rows 6N   �� *SelectedRows+5�      ��5�      �� 5�      .R�5�      �� $��Center=�6)   $��Center=�6F   .+�5�   "   �� 
���   �$	��200��6=   $	��200��6I   ��5�   )   �� *DataContext.CardClicked+ �� RelativeSource ��  �� AncestorType
 Window.��5�   &   �� $��?��60   $��Center=�69   $��Center=�6V   $��20(�6q   
�������6   5�   #   5�      5�      5�      5�      5�      5�      5�      