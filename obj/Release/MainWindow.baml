   M S B A M L   `   `   `  ���� ?  ;TM2, Version=1.0.0.26, Culture=neutral, PublicKeyToken=nullclr-namespace:TM2TM2      TM2.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     !tabclr-namespace:TM2     -     � Title$  Mans meklētājs��$��Calibri)� � Icon$ Images/Logo.ico�� � WindowState$ 	Maximized=�$��14.667(�$
��1200�� � 	Resources  �� MergedDictionaries �� Source��  �� $$ StyleDictionarys/GroupBox.xaml?��� $' !StyleDictionarys/ContextMenu.xaml?��� $( "StyleDictionarys/ScrollViewer.xaml?��� $' !StyleDictionarys/ProgressBar.xaml?��� $&  StyleDictionarys/TabControl.xaml?��� $" StyleDictionarys/Button.xaml?��� $# StyleDictionarys/TextBox.xaml?��� $$ StyleDictionarys/ComboBox.xaml?�%    .��   TM2.TabControlEx  -     Name$ 
tabControl��.@���    Main#��[  ���� $ ��Images/TabControl/home.png��$��20�� J� OpacityMask �	#FF333333.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��0,0,0,2q����	#FFFF946B._�]� -   $��navigationPanel��$��0��$��0��
�������.+��� -   $��MainMenu��  
Navigation#��[ $	��116��.��� $	��4,0q� �� $��* ��� $��14 �.+��� $��0��	 �� Orientation$	 
Horizontal=�.+��� -   $��btnBack��  Red#��[ $y�	Collapsed=�.���  �� $��* ��� $��24 �.+��� $��0��$��64��$/��)/TM2;component/Images/Navigation/back.png���� $��1��$	 
Horizontal=�$��Center=�.+��� $��Atpakaļ��$��Center=�$��Center=��� -   $��
borderBack��$y�	Collapsed=�$��1q�
 �� Padding$
 0q�$��Center=�$
��4,12q�$��1����L�  j� Viewport$ 0,0,8,8� j� ViewportUnits$ Absolute=� j� TileMode$
 Tile=� L� Drawing I� .���  � Brush
 ����� � Geometry � .����  �� Rect$ 	0,0,50,50��� $ 50,50,50,50��� -   $��btnToday�� � Cursor$
 Help��{�.  .���  �� $��* ��� $��24 �.+��� $��0��$��64��$3��-/TM2;component/Images/Navigation/calendar.png���� $��1��$	 
Horizontal=�$��Center=�.+��� -   $��txtToday��$��	Šodienas��
 � Tag$  ��$��Center=�$��Center=��� -	   $��	btnMovies��.���  �� $��* ��� $��24 �.+��� $��0��$��64��$1��+/TM2;component/Images/Navigation/movies.png���� $��1��$��Filmas��$��Center=�$��Center=��� -
   $��btnTV��.���  �� $��* ��� $��24 �.+��� $��0��$��64��$-��'/TM2;component/Images/Navigation/tv.png���� $��1��$��TV��$��Center=�$��Center=��� -   $��btnGames��.���  �� $��* ��� $��24 �.+��� $��0��$��64��$0��*/TM2;component/Images/Navigation/games.png���� $��1��$��Spēles��$��Center=�$��Center=��� -   $��btn4k��.���  �� $��* ��� $��24 �.+��� $��0��$��64��$-��'/TM2;component/Images/Navigation/4k.png���� $��1��$��Ultra HD��$��Center=�$��Center=��� $��1��$��Galvenā izvēlne��$��Center=����	#FFFF946B$
��Bold"�$��12(�� -   $��
datePicker��$��Relative=���.   � 	StaysOpen .  .��� $
��0,-3q�.+�&   System.Windows.Controls.Calendar  -   $��calendar��
����������	#FFFF946B   FirstDayOfWeek$ Monday=��� -   $��TorrentSearch��#��[ $	��116��.���  �� $��* ��� $��14 �.+��� $	 
Horizontal=�$��0��.+�� $��4,4,4,0q�$��Center=� �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� $��Kategorija: ��$��0��$��0��$��12(�$��Center=�$��0q�$��Right=�$
��Bold"��� -   $��cmbCategory��$��0��$��1�� �� SelectedIndex$ 0���� $��Filmu žanrs: ��$��1��$��0��$��12(�$��Center=�$��0q�$��Right=�$
��Bold"��� -   $��cmbGenre��$��1��$��1��$ 0��$	��0,7q�{�.  �� $��Gads: ��$��2��$��0��$��12(�$��Center=�$��0q�$��Right=�$
��Bold"��� -   $��cmbYear��$��2��$��1��$ 0��{�.  �� $��1q�$
 0q�$��Center=�$
��0,14q�$��1����L� $ 0,0,8,8�$ Absolute=�$
 Tile=� I� .��� 
 ����� � .���� $ 	0,0,50,50��� $ 50,50,50,50��� $��Center=�$	��4,0q�.+��� -   $��	CmbMeklet��  ComboBoxMeklet#��[ $��36��$��2,1,1,1q�$	��250�� �� 
IsEditable .  �� IsTextSearchEnabled .  {�.  �� -   $��	BtnMeklet��$	��248��$��36��$��1q�{�.  .��� $��Center=�$��0,0,10,0q� �� $��24 ��� $��40 �.+��� $��0��$1��+/TM2;component/Images/Navigation/search.png��$��24��$��24���� $��1��$��Meklēt��$��Center=�$��Center=�$��0,0,0,5q��� $��Torrentu meklēšana��$��1��$��Center=����	#FFFF946B$
��Bold"�$��12(��� -   $��Trackers��#��[ $	��116��.��� $	��4,0q� �� $��* ��� $��14 �.+��� -   $	 
Horizontal=�.+��� -   $��btnKonti��.���  �� $��* ��� $��24 �.+��� $��0��$��64��$/��)/TM2;component/Images/Navigation/user.png���� $��1��$��Konti��$��Center=�$��Center=��� -   $��spliter��$y�	Collapsed=�$��1q�$
 0q�$��Center=�$
��4,12q�$��1����L� $ 0,0,8,8�$ Absolute=�$
 Tile=� I� .��� 
 ����� � .���� $ 	0,0,50,50��� $ 50,50,50,50��� -   $��groupBoxFano��$y�	Collapsed=�  Tracker#��[ $��Bottom=�$��Center=�$��95��.��� .+�� -   $��progressBarFano��$y�Hidden=� � IsIndeterminate . $��74��$��74�� J� RenderTransformOrigin$ 0.5,0.5&�$��0q�$��0q���Z� ��Z� z�V� .~��� �� ��  �� Angle$ 90Q�T� ����  �� EndPoint$	 0,0&� � MappingMode$ RelativeToBoundingBox=� �� 
StartPoint$	 1,0&�.���   � Color$  	#FFFBB296��! � Offset$! 0Q�� $  Transparent��$! 0.235Q��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $��37 ��� $��37 �.+��� -   $��btnFanoClose�� 
 btnTab#��[ $y�Hidden=�$��0��$��1��$��Right=�$
 Hand��" � ToolTip$" Atvienot���� $��1��$��2��$��16��$0��*/TM2;component/Images/Trackers/fano.in.png��$��Center=�$��0q��� $��Fano.in��$��2��$��2��$��4,0,0,0q�$��Center=�$��12(��� -   $��txtFanoUser��$��3��$��2��$��Center=�$
��Bold"�$��12(��� -   $��txtFanoRatio��$��4��$��2��$��Center=�$��12(��� -   $��groupBoxKinozal��$y�	Collapsed=�#��[ $��Bottom=�$��Center=�$��95��.��� .+�� -   $��progressBarKinozal��$y�Hidden=� . $��74��$��74��$ 0.5,0.5&�$��0q�$��0q���Z� ��Z� z�V� .~��� �� �� $ 90Q�T� ���� $	 0,0&�$ RelativeToBoundingBox=�$	 1,0&�.��� $  	#FFFBB296��$! 0Q�� $  Transparent��$! 0.235Q��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $��36 ��� $��2 ��� $��36 �.+��� -    $��btnKinozalClose��#��[ $y�Hidden=�$��0��$��1��$��2��$��Right=�$
 Hand��$" Atvienot���� $��1��$��3��$��16��$3��-/TM2;component/Images/Trackers/kinozal.tv.png��$��Center=�$��0q��� $��2��$��3��$��
Kinozal.tv��$��4,0,0,0q�$��Center=�$��12(��� -!   $��txtKinozalUser��$��3��$��3��$��Center=�$
��Bold"�$��12(��� -"   $��txtKinozalRatio��$��4��$��2��$��Right=�$��0,0,4,0q�$��12(��� -#   $��sp��$y�Hidden=�$��4��$��1��$��|��$��Center=�$��9.333(�$��0,1,0,0q�
���   ��� -$   $��txtAvailable��$��4��$��2��$
��Left=�$��4,0,0,0q�$��12(��� -%   $��groupBoxFilebase��$y�	Collapsed=�#��[ $��Bottom=�$��Center=�$��95��.��� .+�� -&   $��progressBarFilebase��$y�Hidden=� . $��74��$��74��$ 0.5,0.5&�$��0q�$��0q���Z� ��Z� z�V� .~��� �� �� $ 90Q�T� ���� $	 0,0&�$ RelativeToBoundingBox=�$	 1,0&�.��� $  	#FFFBB296��$! 0Q�� $  Transparent��$! 0.235Q��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $��37 ��� $��37 �.+��� -'   $��btnFilebaseClose��#��[ $y�Hidden=�$��0��$��1��$��Right=�$
 Hand��$" Atvienot���� $��1��$��2��$��16��$4��./TM2;component/Images/Trackers/filebase.ws.png��$��Center=�$��0q��� $��2��$��2��$��Filebase.ws��$��Center=�$��12(��� -(   $��txtFilebaseUser��$��3��$��2��$��Center=�$
��Bold"�$��12(��� -)   $��txtFilebaseRatio��$��4��$��2��$��Center=�$��12(��� $��Trakeri��$��1��$��Center=����	#FFFF946B$
��Bold"�$��12(��� -*   $��Other��#��[ $	��116��.��� $	��4,0q� �� $��* ��� $��14 �.+��� $	 
Horizontal=�$��0��$��Right=�.+��� -+   $��btnIespejas��.���  �� $��* ��� $��24 �.+��� $��0��$��64��$2��,/TM2;component/Images/Navigation/options.png���� $��	Iespējas��$��1��$��Center=�$��Center=��� $
��Cits��$��1��$��Center=����	#FFFF946B$
��Bold"�$��12(�� -,   $��progressBar��$��1��$y�	Collapsed=�$��8��# J� IsHitTestVisible# .   . 
������ ��Z� ���� $ 0.5,1&�$ RelativeToBoundingBox=�$ 0.5,0&�$ �� RelativeTransform$ V� .~��� % �� CenterY$	% 0.5Q�& �� CenterX$	& 0.5Q��� ' �� CenterY$	' 0.5Q�( �� CenterX$	( 0.5Q��� $ 90Q�) �� CenterY$	) 0.5Q�* �� CenterX$	* 0.5Q�T� .��� $  	#FFFF946B��$! 0.246Q�� $  Transparent��$! 1Q��� --   $��filterPanel��$y�	Collapsed=�$��2�� 
 Filter#��[ $
��Left=��� -.   $��filterPanelCategorys��$y�	Collapsed=�$��2��#��[ $
��Left=�� -/   $��containerList��$��3��� -0   $��containerCovers��$��3���� #��[ $��5��.��� $��4q� �� $��* ��� $��* �.+��� -1   $��publishVersion��$��0��$
��Left=�$��12(��� -2   $��imgWorld��$��1��$	" www��$��16��$��Images/Other/world.png��$��Right=�$
 Hand��