   M S B A M L   `   `   `  ����D  @FirstTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:FirstTask	FirstTask  5          FirstTask.TrackControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:FirstTask     5      .��5	      �� -   5          ��Name$  TrackBorder��5	   
   
������  �� Padding$ 10q�68   $��5q�6E    � Cursor$
 Hand��6P   ��5
      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value
 ���� 6#    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � �� �#0909096%   5      5      ._�5      � 	 � ColumnDefinitions	 5      �� 5      $��30 �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5      �� -   5        
 ��Name$
 tbTrackNumber��5      $��1��6&   
�������$��0,0,10,0q�6B   $��Center=�6T   �� 5      $��1��6    �� Orientation$ 
Horizontal=�6    .+�5      � $��60��6   $��60��6   }�. .+�5      ��  �� CornerRadius$ 20��6   $��5q�6    ._�5      �� -   5         ��Name$ 
TrackImage��5      $��Images/placeholder.jpg��5        $��Uniform=�5!       5"      �� 5#      $��20��6   $��20��6   
������ 
������ $��Right=�6X   $	��Top=�6t   $��0,0,5,0q�6�    J� Opacity$	 0.5Q�6�   ��5$      �� 5%   	    �� 
TargetType" ��6   .R�5&   
   �� 
������ $ 10��6*   $ 5q�6<   ._�5'      �� $	��...��6   $��16(�6    
�������$��Center=�6A   $��Center=�6\   5(      5)   
   5+      5,      �� 5-      $��10,0,0,0q�6   .+�5.      �� -   5        $
 tbTrackTitle��5.      $��luther (with sza)��6'   $
��Bold"�6@   
�������6R   �� 5/      -   5        $
 tbTrackArtist��5/      $��Kendrick Lamar, SZA��6(   $��12(�6C   ���#9B9B9B6Q   50      51      �� 52      -   5         ��Name$ AnimationPlaceholder��52      $��1��62   $��40��6B   $��40��6M   $
��Left=�6Y   $��Center=�6t   $��10,0,0,0q�6�   �� 53      -   5        $
 tbTrackDuration��53      $��3��6(   $��03:20��68   $��Right=�6E   $��50��6a   ���#9B9B9B$��	10,0,10,0q�6�   54      55      56      