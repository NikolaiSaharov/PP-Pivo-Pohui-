   M S B A M L   `   `   `  ����D  @FirstTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:FirstTask	FirstTask  5          FirstTask.PlaylistPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.8.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      #localclr-namespace:FirstTask  5      .  (MaterialDesignThemes.Wpf.ColorZoneAssist    Mode$  
PrimaryMid=�5       N� Title$ PlaylistPage��5      .��5      � .+�5   
   ��  �� Orientation$ Vertical=�6   $
��Left=�6,   $��
70,87,0,90q�6G   .+�5      �� 
�������$	��351��6'   $	��351��63    �� CornerRadius$ 20��6@   ._�5      � .+�5      �� $��Images/placeholder1.jpg��6   $��Uniform=�6=   �� 5      -   5         ��Name$ EllipsisButton��5      $��38��5      $��38��6(   ���	#80000000
������ $��Right=�5      $	��Top=�5      $��0,5,5,0q�5       J� Opacity$ 0Q�5      ��5      �� 5       �� 
TargetType" ��6.   .R�5    "   �� #��z��6)   $ 19��5!   )    �� Padding$ 5q�5"   )   ._�5#   &   �� $	��...��60   $��16(�5$   0   
�������$��Center=�5&   0   $��Center=�5'   0   5(   #   5)      5+      5,      5-      �� 5.      $��Мой плейлист��6   $��Inter)�6,   $
��Bold"�6?   $��21(�6Q   
�������$��	90,25,0,0q�6r   $��31��6�   �� 5/      $8��225 треков, примерно 1 ч. 25 мин.��6   $��Inter)�6@   $
��Bold"�6S   $��18(�6e   ���#9B9B9B$��
25,15,0,10q�6�   �� 50      $ 
Horizontal=�6   $	��413��62   $	��186��6?   .+�52      �� $��Слушать��6   $	��130��53      $��43��6%   
���   �   MaterialDesignRaisedButton#��[  55      ���	#FF9BC2DB���	#FF91BCD7$��20(�57      $��Inter)�6'   $
��Bold"�6:   $��0,0,0,0q�6L   ��58      �� 59      " ��6*   .R�5:      �� ���#9BC2DB$ 20��6:   $ 10q�6L   ._�5;   "   �� $��Center=�63   $��Center=�6P   5<      5=      5?      �� 5B      $��43��6   $��43��6$   
���   �#��[  5D      ���	#FF9BC2DB���	#FF91BCD7$��10,0,0,0q�5F      ��5G      �� 5H      " ��6*   .R�5I      �� ���#9BC2DB$ 20��6:   $ 10q�6L   ._�5J   "   �� $��Center=�63   $��Center=�6P   5K      5L      .��5N      �� $/��)pack://application:,,,/Images/shuffle.jpg��6   $��23��6O   $��23��6[   5O      5P      5Q      � 5T   
   $
��Left=�6   $
��1158��6*   $��
619,40,0,0q�67    � RowDefinitions 5V      �� 5W      $
��Auto �6    �� 5X      $��* �6    .+�5Z      � 
������ $��30��6,   	 � ColumnDefinitions	 5[      �� 5\      $��30 �6'   �� 5]      $��* �6'   �� 5^      $
��Auto �6'   �� 5_      $
��Auto �6'   .+�5a      �� $��#��6   
�������$��20,0,0,0q�68   $��Center=�6J   �� 5b      $��1��6   $��Название��6,   
�������$��20,0,0,0q�6O   �� 5c      $��2��6   $��Альбом��6,   
�������$��
10,0,580,0q�6M   �� 5d      $��/Images/time.jpg��6   $��459,-5,-52,10q�62   $��2��6I   $��22��6Y   $��22��6d   5e      �� 5f      
�������$��1��6(   $��0,20,0,0q�63   �� 5i      $��1��6   $
��Auto=�6(   .��5j      �� -   5        
 ��Name$
 TracksStackPanel��5j      .+�5l         FirstTask.TrackControl     TrackNumber$ 1��6)      
TrackTitle$ luther (with sza)��69      TrackArtist$ Kendrick Lamar, SZA��6X      TrackDuration$ 03:20��6z      TrackImageSource$ Images/placeholder.jpg��6�     5m      $ 2��6)   $ luther (with sza)��69   $ Kendrick Lamar, SZA��6X   $ 03:20��6z   $ Images/placeholder.jpg��6�     5n      $ 3��6)   $ luther (with sza)��69   $ Kendrick Lamar, SZA��6X   $ 03:20��6z   $ Images/placeholder.jpg��6�     5o      $ 4��6)   $ luther (with sza)��69   $ Kendrick Lamar, SZA��6X   $ 03:20��6z   $ Images/placeholder.jpg��6�     5p      $ 5��6)   $ luther (with sza)��69   $ Kendrick Lamar, SZA��6X   $ 03:20��6z   $ Images/placeholder.jpg��6�     5q      $ 6��6)   $ luther (with sza)��69   $ Kendrick Lamar, SZA��6X   $ 03:20��6z   $ Images/placeholder.jpg��6�     5r      $ 7��6)   $ luther (with sza)��69   $ Kendrick Lamar, SZA��6X   $ 03:20��6z   $ Images/placeholder.jpg��6�     5s      $ 8��6)   $ luther (with sza)��69   $ Kendrick Lamar, SZA��6X   $ 03:20��6z   $ Images/placeholder.jpg��6�     5t      $ 9��6)   $ luther (with sza)��69   $ Kendrick Lamar, SZA��6X   $ 03:20��6z   $ Images/placeholder.jpg��6�   5u      5v      5w      5x      5y      