   M S B A M L   `   `   `  ���� ?  ;TM2, Version=1.0.0.26, Culture=neutral, PublicKeyToken=null+clr-namespace:TM2.ControlsTM2.Controls      TM2.Controls.TorrentList   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:TM2.Controls     $��Calibri)�$��12(�  � 	Resources   �� MergedDictionaries �� Source��  �� $3 -/TM2;component/StyleDictionarys/ListView.xaml?�%    .���� -   $��tList��
������ $
��10,0q�$
��-3,0q� �� View �� .P���  �� Width$
 Auto�� �� HeaderContainerStyle ��  �� 
TargetType" ��.���  �� Property �@�OverridesDefaultStyle �� Value . ��  � �� �� 	 �� 
TargetType"	 ��.R�� 
�������.+��� ���	#FFFF946B$��0,1,0,1q��� 
���   �$��0,0,1,0q�
 �� CellTemplate
 �� .R��� $��action��-    � ToolTip$ Lejuplādēt��$y�Hidden=�
 � Tag �� *Name+.���� $.��(/TM2;component/Images/Other/download.png�� �� *DownloadUri+ �� Triggers ��  �� Value$
 True�� �� Binding ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" �� �� Path$ IsMouseOver�.a���  �� 
TargetName$ action�� � y�$ Visible=��� $ 34������ -   $ Tracker��$��Center=�.��#
 �� .R��� -   $
 Info��$��0q� �� *Name+.���� $��16��$��Center=�$
 Info�� � Cursor$
 Hand������ *	Tracker+ �� *
DetailsUri+�� ���� -   $
 Name��.��	Nosaukums
 �� .R��� .+���  �� 
IsReadOnly . $��0q�
������  �� SelectionBrush
 ��������� *Name+�� $��0q�$��12(� J� IsHitTestVisible .  
������ 
���  ������ *Type+ Z� ��Z� �� ���� -   $ FileSize��$��80��$��Center=�.��	Izmērs
 �� .R��� $��Center=����� *Size+�� ���� -   $
 Seed��$��60��$��Center=�.��	Devēji
 �� .R��� $��Center=�
��� � ����� *Seed+�� ���� -   $ Leech��$��60��$��Center=�.��	Ņēmēji
 �� .R��� $��Center=�
���  ������ *Leech+�� ���� -	   $
 Date��$��80��$��Center=�.��Datums
 �� .R��� $��Center=����� $
 Date� �� StringFormat$ 
dd/MM/yyyy���� ���� -
   $ Comments��$��80��$��Center=�.��
Komentāri
 �� .R��� $��action��-   $��Center=� . $��0q�
������ $
 Hand������ *
Comments+ �� *
DetailsUri+ �� *Name+.������ " �� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  �@��TextDecorations$ 	Underline{�.��