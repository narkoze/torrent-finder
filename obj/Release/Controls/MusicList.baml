   M S B A M L   `   `   `  ���� ?  ;TM2, Version=1.0.0.18, Culture=neutral, PublicKeyToken=null+clr-namespace:TM2.ControlsTM2.Controls      TM2.Controls.MusicList   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:TM2.Controls     $��Calibri)�$��12(�  � 	Resources   �� MergedDictionaries �� Source��  �� $3 -/TM2;component/StyleDictionarys/ListView.xaml?��� $1 +/TM2;component/StyleDictionarys/Slider.xaml?�%    .���� -   $��mList��
������ $
��10,0q�$
��-3,0q� �� View �� .P���  �� Width$
 Auto�� �� HeaderContainerStyle ��  �� 
TargetType" ��.���  �� Property �@�OverridesDefaultStyle �� Value . ��  � �� �� 	 �� 
TargetType"	 ��.R�� 
�������.+��� ���	#FFFF946B$��0,1,0,1q��� 
���   �$��0,0,1,0q�
 �� CellTemplate
 �� .R���  �� Orientation$ 
Horizontal=�.+��� $��btnDownload�� � ToolTip$ Lejuplādēt��$y�Hidden=�
 � Tag �� *Name+.���� $.��(/TM2;component/Images/Other/download.png�� �� *DownloadUri+�� $��btnPlay��-   $ 	Atskaņot��.���� $*��$/TM2;component/Images/Music/play.png�� �� *DownloadUri+ �� Triggers ��  �� Value$
 True�� �� Binding ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" �� �� Path$ IsMouseOver�.a���  �� 
TargetName$ btnDownload�� � y�$ Visible=��� $ 34������ $
 Site��$��Center=�.��#
 �� .R��� $
 Info��$��0q� �� *Name+.���� $��24��$��Center=�$
 Info�� � Cursor$
 Hand������ *Site+ �� *
DetailsUri+�� ���� $
 Name��.��	Nosaukums
 �� .R��� .+���  �� 
IsReadOnly . $��0q�
������  �� SelectionBrush
 ��������� *Name+�� $��Slider��$y�	Collapsed=��� ���� $ FileSize��$��80��$��Center=�.��	Izmērs
 �� .R��� $��Center=����� *Size+�� ���� $ Length��$��60��$��Center=�.��Garums
 �� .R��� $��Center=�
��� � ����� *Seed+�� ���� $ Quality��$��60��$��Center=�.��
Kvalitāte
 �� .R��� $��Center=�
���  ������ *Leech+