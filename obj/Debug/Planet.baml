   M S B A M L   `   `   `  ����D  @FirstTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:CHOTOPOHOZEENASPOTIKCHOTOPOHOZEENASPOTIK  5      !    CHOTOPOHOZEENASPOTIK.Planet   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.8.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .local"clr-namespace:CHOTOPOHOZEENASPOTIK  5        N� Title$  
PlanetPage��5
       � Cursor$  ��6    � 	Resources 5         MoodButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers	 P� Property
 P� Value  RoundedButtonStyle  CustomSliderStyle ��Name'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� 
TargetName �� TargetProperty W� From g� Duration	 W� To �� RelativeSource �� Path n�Name K� IsItemsHost �� Padding � ColumnDefinitions��%  &	        &	 �    &	 c    '�� �    'm� y	    �� 5   
   " ��6(   .�5      ��  � ��
 ���� 6+   �� 5       � ��
 �����6+   �� 5       � ��$ Inter)�6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 16(�6)   �� 5       � ��
 ���� 6,   �� 5       � ��$
 15,5q�6(   �� 5       � ��$ 15q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   $��2q�5   !   $ 15��5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       	 �@J�IsMouseOver
 . . �5!   "   ��  � �� �	#33FFFFFF6?   5"      P� 5#      	 � ��
 . . �5$   "   ��  � �� �	#66FFFFFF6?   5%      5'      5)      5*      �� 5,   
   " ��6+   .�5-      ��  � �� �#1717176+   �� 5.       � ��
 �����6+   �� 5/       � ��
 ���� 6,   �� 50       � �� 51      �� 52      " ��6&   .R�53      �� #��z��6!   $ 15��54   !   #��z��55   !   #��z��56   !   ._�57      �� $��Center=�6/   $��Center=�6L   58      59      5;      5<      �� 5>   
   " ��6*   .�5?      ��  � ��
 �����6+   �� 5@       � ��
 ��  �6+   �� 5A       � ��
 ���� 6,   �� 5B       � �� 5C      �� 5D      " ��6&   .R�5E      � .+�5F      �� #��z��6%   $ 10��5G   %   #��z��5H   %   #��z��5I   %   $��10��5J   %   $��Center=�5K   %   �� 5L      $ 	PART_Fill��6%   #��z��5M   %   $ 10��5N   %   $��10��5O   %   $��Center=�5P   %   $
��Left=�5Q   %    5R   "     5S   &   
FillStates��. 5T   *     
Normal��. 5U   .   �� .��5V   2   W� $ 	PART_Fill��6B   $ Width�5W   B   $ 0X�5X   B   $ 0:0:0.1D�5Y   B    5X   K   ��  �� *TemplatedParent+$ Value�5Y   B   5Z   /   5[   +   5\   '   5^      n� 5_      $ Thumb��6$   $��20��5`   $   $��20��5a   $   $��Center=�5b   $   $
��Left=�5c   $   
�������5d   $   5e      5f      5h      5i      �� 5l   
   " ��6   .�5m      ��  � �� �	#FF1E1E1E6+   �� 5n       � ��
 ���� 6,   �� 5o       � ��$ 0q�60   �� 5p       � ��$ 5q�6(   �� 5q       � �� 5r      �� 5s      " ��6&   .R�5t      �� #��z��6!   #��z��5u   !   #��z��5v   !   $ 10��5w   !   ._�5x      ��  . 6)   5y      5z      5|      5}      �� 5�   
   " m�6   .�5�      ��  � ��
 ���� 6#   �� 5�       � ��
 �����6#   �� 5�       � ��$ Inter)�6#   �� 5�       � ��$ 13(�6!   �� 5�       � ��$
 10,5q�6    �� 5�       � �� 5�   
   �� 5�      " m�6   .R�5�      �� #��z��6   # z��5�      $ 5��5�      ._�5�      �  5�      �� 5�      $��24 �6/   �� 5�      $��* �6/   .+�5�      �� $
��Icon��6+   $��0��6@   $��Center=�6P   $��0,0,10,0q�6k   �� 5�      $��Header��6+   $��1��6B   $��Center=�6R   5�      5�       5�      P� 5�      	 �@J�IsMouseOver
 . . �5�      ��  � �� �	#33FFFFFF67   5�      5�      5�      5�      5�      .��5�      � .+�5�   
   �� -   5         ��Name$ BackgroundRect��5�       K� ZIndex$ 0��6,   ��5�      �� 5�      -   5        BackgroundGradient�� �� 
StartPoint$	 0,0&�5�   B    �� EndPoint$	 1,1&�6S   .��5�      �  � Color$ #B700FF��6#    � Offset$ 0Q�63   � 5�      $ #0028FF��6#   $ 1Q�63   � 5�      $ #46CFFF��6#   $ 1Q�63   � 5�      $ #511CE8��6#   $ 1Q�63   � 5�      $ #AA91FF��6#   $ 1Q�63   � 5�      $ #4E73FF��6#   $ 1Q�63   � 5�      $ #D200FF��6#   $ 1Q�63   � 5�      $ #0010C8��6#   $ 1Q�63   � 5�      $ #8800FF��6#   $ 1Q�63   5�      5�      �� 5�   
   $,��&pack://application:,,,/Images/logo.jpg��6   $��28��5�      $��45��6   $	��Top=�5�      $
��Left=�6(   $��	10,10,0,0q�6C   �� 5�   
     �� Orientation$  
Horizontal=�6   $
��Left=�6.   $	��Top=�6I   $��-27,108,0,0q�6a   ! J� RenderTransformOrigin$! 0.5,0.5&�6v   z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      " �� Angle$" 90Q�6&   T� 5�      5�      .+�5�      A� -   5        # A�Name$# Level1Indicator��5�   E   $��10��6   $��10��6!   
�������$��5q�6:   6E   A� 5�      -   5        $# Level2Indicator��5�   E   $��10��6   $��10��6!   
�������$��5q�6:   6E   A� 5�      -   5        $# Level3Indicator��5�   E   $��10��6   $��10��6!   
�������$��5q�6:   6E   A� 5�      -   5        $# Level4Indicator��5�   E   $��10��6   $��10��6!   
�������$��5q�6:   6E   A� 5�      -   5        $# Level5Indicator��5�   E   $��10��6   $��10��6!   
�������$��5q�6:   6E   5�      �� 5�   
   -   5        $ ��Name$$ LevelScrollViewer��5�      $��Hidden=�62   $��Hidden=�6W   .��5�      � -	   5        % �Name$% ContentGrid��5�   $   $ 2��6   & � RowDefinitions& 5�      �� 5�      $
��Auto �6$   $	��464��62   �� 5�      .+�5�      � -
   5        $% Level1��5�      $y�Visible=�6'   .+�5�      �� $��0��6!   $��0,20,300,20q�6.   $  
Horizontal=�6C   $��Center=�6\   $	��542��6y   .+�5�      #  MaterialDesignThemes.Wpf.Card  $	��266��6.   $	��266��6:   $��10q�6G   ���	#FF2F2F2F.��5�      � & 5�   "   �� 5�   &   $	��160 �64   �� 5�   &   $��* �64   .+�5�   "   �� $3��-pack://application:,,,/Images/placeholder.jpg��6(   $��UniformToFill=�6_   $��0,0,0,2q�6w   $��2��6�   5�      5�      �� 5�      $��Center=�6%   $��20,0,0,0q�6@   .+�5�      �� $  Vertical=�6)   $��	0,10,0,20q�6@   .+�5�   "   �� $��Плейлист��6,   ���#A6A6A6$��18(�6Q   $��Inter)�6_   $
��Bold"�6r   $��52��6�   �� 5�   "   $��Название��6,   
�������$��30(�6O   $��Inter)�6]   $
��Bold"�6p   $��0,5,0,0q�6�   $��37��6�   �� 5�   "   $��Описание��6,   ���#A6A6A6$��28(�6Q   $��Inter)�6_   $
��Bold"�6r   $��0,10,0,0q�6�   $��40��6�   �� 5�   "   $��100 песен��6,   ���#A6A6A6$��17(�6R   $��Inter)�6`   $
��Bold"�6s   $��0,10,0,0q�6�   $��59��6�   5�      �� 5�      $  
Horizontal=�6)   .+�5�   "   �� -   5        $��Слушать��5�   )   $	��130��5�   )   $��43��65   
�������#��[ 5�   )   ���#8393FE���#8393FE$��20(�6�   $��Inter)�6�   $
��Bold"�6�   6�   �� 5�   "   $��43��6)   $��43��64   
���   �#��[ 5�   )   ���#8393FE���	#FF91BCD7$��10,0,0,0q�5�   )   .��5�   &   �� $/��)pack://application:,,,/Images/restart.jpg��6,   $��23��5�   ,   $��23��68   5�   #   �� 5�   "   $��43��6)   $��43��64   $��10,0,0,0q�5�   )   
������ 
������ $��0q�5�   )   .��5�   &   �� $,��&pack://application:,,,/Images/plus.jpg��6,   $��35��5�   ,   $��35��67   5�   #   5�      5�      5�      5�      � 5�      -   5        $% Level2��5�      $y�	Collapsed=�6'   .+�5�      �� $��0��6!   $��0,20,300,20q�6.   $  
Horizontal=�6C   $��Center=�6\   $	��542��6y   .+�5�        $	��266��6.   $	��266��6:   $��10q�6G   ���	#FF2F2F2F.��5      � & 5  "   �� 5  &   $	��160 �64   �� 5  &   $��* �64   .+�5  "   �� $3��-pack://application:,,,/Images/placeholder.jpg��6(   $��UniformToFill=�6_   $��0,0,0,2q�6w   $��2��6�   5     5     �� 5	     $��Center=�6%   $��20,0,0,0q�6@   .+�5
     �� $  Vertical=�6)   $��	0,10,0,20q�6@   .+�5  "   �� $��Микс дня��6,   ���#A6A6A6$��18(�6Q   $��Inter)�6_   $
��Bold"�6r   $��52��6�   �� 5  "   $��Название��6,   
�������$��30(�6O   $��Inter)�6]   $
��Bold"�6p   $��0,5,0,0q�6�   $��37��6�   �� 5  "   $��Описание��6,   ���#A6A6A6$��28(�6Q   $��Inter)�6_   $
��Bold"�6r   $��0,10,0,0q�6�   $��40��6�   �� 5  "   $��100 песен��6,   ���#A6A6A6$��17(�6R   $��Inter)�6`   $
��Bold"�6s   $��0,10,0,0q�6�   $��59��6�   5     �� 5     $  
Horizontal=�6)   .+�5  "   �� -   5        $��Слушать��5  )   $	��130��5  )   $��43��65   
�������#��[ 5  )   ���#8393FE���#8393FE$��20(�6�   $��Inter)�6�   $
��Bold"�6�   6�   �� 5  "   $��43��6)   $��43��64   $��10,0,0,0q�5  )   
������ 
������ $��0q�5  )   .��5  &   �� $,��&pack://application:,,,/Images/plus.jpg��6,   $��35��5  ,   $��35��67   5  #   5     5      5!     5"     � 5#     -   5        $% Level3��5#     $y�	Collapsed=�6'   .+�5$     �� $8��2Рекомендую к прослушиванию��6    
�������$��Inter)�6U   $
��Bold"�6h   $��24(�6z   $��Center=�6�   $��Center=�6�   $��	0,0,0,350q�6�   � 5%     $��Center=�6    $��Center=�6=   .+�5&       $	��250��6.   $	��250��6:   $��20,100,50,0q�6G   
�������6\     5'     $	��250��6.   $	��250��6:   $��100,100,50,0q�6G   
�������6]     5(     $	��250��6.   $	��250��6:   $��100,100,50,0q�6G   
�������6]   5)     5*     � 5+     -   5        $% Level4��5+     $y�	Collapsed=�6'   .+�5,     �� $D��>Как бы вы описали своё настроение?��6    
�������$��Inter)�5.      $
��Bold"�5/      $��24(�50      $��Center=�51      $��Center=�52      $��	0,0,0,350q�53      �� 54     ���	#4D000000$ 30��64   $��470,430,470,0q�6F   $ 0q�6]   ._�55     � $��Center=�6$   $��Center=�6A   .+�56     �� -   5        $��Ужасное��56  %   #��[  67   6`   �� 57     -   5        $��Плохое��57  %   #��[  66   6_   �� 58     -   5        $��Тревожное��58  %   #��[  69   6b   �� 59     -   5        $��Безразличное��59  %   #��[  6<   6e   �� 5:     -   5        $��Смешанное��5:  %   #��[  69   6b   �� 5;     -   5        $��Хорошее��5;  %   #��[  67   6`   �� 5<     -   5        $��Прекрасное��5<  %   #��[  6:   6c   5=     5>     5?     � 5@     -   5        $% Level5��5@     $y�	Collapsed=�6'   .+�5A     �� $?��9Музыка, которая чувствует тебя.��6    
�������$��Inter)�5C      $
��Bold"�5D      $��24(�5E      $��Center=�5F      $��Center=�5G      $��	0,0,0,350q�5H      �� 5I     $��С чего начнём?��6    
�������$��24(�5K      $��Inter)�5L      $
��Bold"�5M      $��Center=�5N      $��Center=�5O      $��	0,0,0,280q�5P      � 5Q     $��Center=�6    $��Center=�6=   $��	0,350,0,0q�6X   .+�5R     �� ���	#4D000000$ 30��68   $	 7,0q�6J   $��10q�6X   ._�5S     �� -   5        $��Создай ремикс��5S  %   #��[  6=   6f   5T     �� 5U     ���	#4D000000$ 30��68   $	 7,0q�6J   $��10q�6X   ._�5V     �� -   5        $!��Распознай трек��5V  %   #��[  6>   6h   5W     �� 5X     ���	#4D000000$ 30��68   $	 7,0q�6J   $��10q�6X   ._�5Y     �� -   5        $&�� Расскажи про трек��5Y  %   #��[  6A   6k   5Z     �� 5[     ���	#4D000000$ 30��68   $	 7,0q�6J   $��10q�6X   ._�5\     �� -   5        $��	TuneShift��5\  %   #��[  69   6c   5]     5^     5_     � 5b     $��1��6   $��	0,20,0,10q�6$   & 5c     �� 5d     $
��Auto �6(   �� 5e     $��* �6(   .+�5h     �� $��0��6   
���   �._�5i     � $	��880��6   $
��Left=�6+    5j     �� 5k  "   $
��Auto �63   �� 5l  "   $
��Auto �63   �� 5m  "   $
��Auto �63   �� 5n  "   $
��Auto �63   �� 5o  "   $
��Auto �63   �� 5p  "   $
��Auto �63   .+�5r     �� -   5        $��Все��5r  %   #��[ 5s  %   $��35��5t  %   $��80��5u  %   
����������#171717$��	0,5,248,0q�5y  %   $	��Top=�68   �� 5z     -   5        $��Музыка��5z  %   #��[ 5{  %   $��35��5|  %   $	��106��5}  %   
�������$��
84,5,138,5q�5�  %   �� 5�     -   5        $��
Посты��5�  %   #��[ 5�  %   $��35��5�  %   $��95��5�  %   
�������$��235,5,-330,5q�5�  %   $��5��6;   �� 5�     -   5        $��
Клипы��5�  %   #��[ 5�  %   $��35��5�  %   $	��123��5�  %   
�������$��
195,5,10,5q�5�  %   �� 5�     -    5        $��Подкасты��5�  %   #��[ 5�  %   $��35��5�  %   $	��138��5�  %   
�������$��323,5,-133,0q�5�  %   $��6��6;   $	��Top=�6O   �� 5�     -!   5        $��Аудио книги��5�  %   #��[ 5�  %   $��35��5�  %   $��92��5�  %   
�������$��138,5,-230,5q�5�  %   $��5��6;   5�     5�     �� 5�     $��1��6#   ���#090909$��	0,0,0,-13q�6E   .��5�     � -"   5        ' �Name$' TracksContainer��5�  $   $��20q�6=   .+�5�       $	��176��62   $	��250��6>   $��5q�6K   ���#0909.��5�  "   � & 5�  &   �� 5�  *   $	��160 �68   �� 5�  *   $��* �68   .+�5�  &   �� $3��-pack://application:,,,/Images/placeholder.jpg��6,   $��UniformToFill=�5�  ,   $	��176��6D   $	��176��6P   �� 5�  &   $��1��61   $��8q�6>   .+�5�  *   �� $!��Название трека��64      MaterialDesignBody1TextBlock#��[ 5�  4   
�������$��14(�5�  G   $��Inter)�6U   $
��Bold"�6h   �� 5�  *   $��Исполнитель��64    " MaterialDesignCaptionTextBlock#��[ 5�  4   
�������$��14(�5�  F   $��Inter)�6T   $
��Bold"�6g   5�  '   5�  #   ( � ContextMenu( 5�  "   �� 5�  &   .@�5�  *   m� -#   5        $ ��Что думает Meret?��5�  3   ) m� Icon) 5�  .   �� 5�  2   $.��(pack://application:,,,/Images/Neyro1.jpg��68   $��14��6j   $��14��6u   5�  +   m� 5�  *   $��Создать микс��63   ) 5�  .   �� 5�  2   $.��(pack://application:,,,/Images/Neyro2.jpg��68   $��14��6j   $��14��6u   5�  +   m� 5�  *   $!��Создать ремикс��63   ) 5�  .   �� 5�  2   $.��(pack://application:,,,/Images/Neyro3.jpg��68   $��14��6j   $��14��6u   5�  +   m� 5�  *   $,��&Добавить в медиатеку��63   ) 5�  .   �� 5�  2   $.��(pack://application:,,,/Images/Neyro4.jpg��68   $��14��6j   $��14��6u   5�  +   m� 5�  *   $(��"Добавить в очередь��63   ) 5�  .   �� 5�  2   $.��(pack://application:,,,/Images/Neyro5.jpg��68   $��14��6j   $��14��6u   5�  +   m� 5�  *   $*��$Микс по исполнителю��63   5�  +   m� 5�  *   $*��$Добавить в плейлист��63   5�  +   m� 5�  *   $��Поделится��63   5�  +   5�  '   5�     5�     5�     5�     5�     5�     5�     5�     