   M S B A M L   `   `   `  ����E  AMemoryGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:MemoryGame.ViewModelsMemoryGame.ViewModels  5   	   !    MemoryGame.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   2vm#clr-namespace:MemoryGame.ViewModels     5   	     � Title$  Sign In��5   	   $	��400��6   $	��500��6&    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5	       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value �� 
TargetName��%}  '��       �� 5
   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $��Border��6!   #��z��5      #��z��5      $��0q�5      $ 5��5      ._�5      �� $��Center=�6/   $��Center=�6L   5      	 5      P� 5      
 � {�$
 True��6;   . �5   "   �� $ Border��6)    � ��
 � d �6S   �� 5   "   $ Border��6)    �@J�Opacity$ 1Q�6P   5      P� 5      
 � ��$
 True��6;   . �5   "   �� $ Border��6)    � ��
 � d �6S   �� 5   "   $ Border��6)    �@J�Opacity$	 0.7Q�6P   5      P� 5!      
 �@J�IsMouseOver$
 True��6=   . �5"   "   �� $ Border��6)    � ��
 � d �6S   �� 5#   "   $ Border��6)    �@J�Opacity$	 0.9Q�6P   5$      P� 5&      
 � {�$ False��6;   . �5'   "   �� $ Border��6)    � ��
 � d �6S   �� 5(   "   $ Border��6)    �@J�Opacity$	 0.5Q�6P   5)      5,      5.      �� 50       � ��$ Segoe Script)�6+   �� 51       � ��$ Italic$�6*   �� 52       � ��$ Light"�6+   �� 53       � ��
 �   �6+   54      55      ��57      �� 58   
    �� ImageSource$ ../Images/background.jpg��6    j� Stretch$
 Fill=�6<    � DataContext 5;      )   #MemoryGame.ViewModels.MainViewModel  5<   
   .��5?      � $��10q�6    � RowDefinitions 5@   
   �� 5A      $��* �6   �� 5B      $
��Auto �6    � ColumnDefinitions 5E   
   �� 5F      $��2* �6   �� 5G      $��3* �6   .+�5J   
   ��  �� DisplayMemberPath$
 Name��5L   
   $��0��5M   
   $��0��6   $��5q�5N   
   ��5J      �� *Users+ �� SelectedItem 5K   
   �� *SelectedUser+��5O      �� 5P       �� Color$
 Gray��6"    �� Opacity$	 0.6Q�6/   ��5R      �� 5S      " ��6   .�5T      ��  � ��$ Segoe Script)�63   �� 5U       � ��$ 13(�61   �� 5V       � ��$ Italic$�62   �� 5W       � ��$ Light"�63   �� 5X       � ��
 �   �63   5Y      5[      � 5]   
   $��0��6   $��1��6   $��5q�6,    5^      �� 5_      $��* �6    �� 5`      $
��Auto �6    �� 5a      $
��Auto �6    .+�5d      �� $��Uniform=�5e      $	��200��5f      $	��200��5g      $��Center=�5h      $��Center=�5i      ��5d      �� *SelectedImage+5i      �� 5k      $��1��6    �� Orientation$ 
Horizontal=�6&   $��Center=�6?   $��0,10,0,0q�6\   .+�5l      �� $	��←��6   $��40��6%   ��60   �� *PreviousImageCommand+�� 5m      $	��→��6   $��40��6%   $��10,0,0,0q�6U   ��60   �� *NextImageCommand+6U   5n      �� 5p      $��2��6   $��Center=�6&   $��10q�6C   .+�5q      �� $)��#Introduceți numele utilizatorului:��6   $��Center=�5r      $��Segoe Script)�5s      $��13(�5t      $��Italic$�5u      $��DemiBold"�5v      
���   �5w      �� 5y      $	��200��5z      $��0,0,0,10q�5{      $��Segoe Script)�5|      $��14(�5}      $��Italic$�5~      $��Light"�5       J� Opacity$	 0.8Q�5�      
�������
���������5y      �� *NewUserName+5�      5�      5�      �� 5�   
   $��2��6   $��2��6"   $ 
Horizontal=�66   $��Center=�6O   $��Bottom=�6l   $��0,20,0,0q�6�   .+�5�      �� $��New User��6   $��20��6K   $��80��6W   $��0,0,30,0q�6b   ��6(   �� *NewUserCommand+6b   �� 5�      $��Delete User��6   $��20��6Q   $��80��6]   $��0,0,30,0q�6�   ��6+   �� *DeleteUserCommand+{�6h   �� *CanPlayOrDelete+6�   �� 5�      $
��Play��6   $��20��6D   $��80��6P   $��0,0,30,0q�6�   ��6$   �� *PlayCommand+{�6[   �� *CanPlayOrDelete+6�   �� 5�      $��Cancel��6   $��20��6H   $��80��6T   $��0,0,0,0q�6`   ��6&   �� *CancelCommand+6`   5�      5�      5�      