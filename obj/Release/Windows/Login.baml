   M S B A M L   `   `   `  ���� ?  ;TM2, Version=1.0.0.26, Culture=neutral, PublicKeyToken=null)clr-namespace:TM2.WindowsTM2.Windows      TM2.Windows.Login   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:TM2.Windows     -     � Title$  Konti�� � WindowStartupLocation$ CenterOwner=� � 
ResizeMode$ NoResize=� � Icon$ /TM2;component/Logo.ico��$	��300��$	��312��$��Calibri)�$��14.667(� � 	Resources  �� MergedDictionaries �� Source��  �� $3 -/TM2;component/StyleDictionarys/Expander.xaml?��� $6 0/TM2;component/StyleDictionarys/PasswordBox.xaml?��� $1 +/TM2;component/StyleDictionarys/Button.xaml?��� $2 ,/TM2;component/StyleDictionarys/TextBox.xaml?��� $6 0/TM2;component/StyleDictionarys/ProgressBar.xaml?�%    .���� .+�9� -   $��expFano����� .+�� -   $��progressBarFano��$y�Hidden=� 	  Login#��[   � IsIndeterminate . $��	-15,0,0,0q�]� .+��� $	��4,0q�$��16��$0��*/TM2;component/Images/Trackers/fano.in.png���� $	��1,0q�$��Fano.in��$
��Bold"�$��Center=��� -   $��imgFanoYesOrNo��$��16��$	��4,0q�$-��'/TM2;component/Images/Login/hand_no.png��$��Right=�$��Right=���]� $��	0,0,0,-15q�.+��� $��Lietotājvārds��$	��Top=��� -   $��txtFano��  	txtFilter#��[ $	��Top=�$	��200��$��30��$
��Left=��� $��Parole��$	��Top=�G� -   $��pwFano��$	��Top=�$	��200��$��30��$
��Left=��� -   $��txtFanoError��$��12(�
���  ��$	��Top=�$��0,2,0,-2q��� -   $��btnFano��  btnLogin#��[ $��Pievienoties��$	��Top=�$
��Left=��� -	   $��btnFanoDisconnect��$y�	Collapsed=�#��[ $��Atvienot��$	��Top=�$
��Left=��� $��Reģistrēties��$	��Top=�$��Right=�$��0,0,4,4q�$��12(�$	��Top=� � ToolTip$I CReģistrēties var tikai ar ielūgumiem (Jautāt izstrādātājam;)��9� -
   $��
expKinozal����� .+�� -   $��progressBarKinozal��#��[  $y�Hidden=� . $��	-15,0,0,0q�]� .+��� $	��4,0q�$��16��$3��-/TM2;component/Images/Trackers/kinozal.tv.png���� $	��1,0q�$��
Kinozal.tv��$
��Bold"�$��Center=��� -   $��imgKinozalYesOrNo��$��16��$	��4,0q�$-��'/TM2;component/Images/Login/hand_no.png��$��Right=�$��Right=���]� $��	0,0,0,-15q�.+��� $��Lietotājvārds��$	��Top=��� -   $��
txtKinozal��#��[ $	��Top=�$	��200��$��30��$
��Left=��� $��Parole��$	��Top=�G� -   $��	pwKinozal��$	��Top=�$	��200��$��30��$
��Left=��� -   $��txtKinozalError��$��12(�
���  ��$	��Top=�$��0,2,0,-2q��� -   $��
btnKinozal��#��[ $��Pievienoties��$	��Top=�$
��Left=��� -   $��btnKinozalDisconnect��$y�	Collapsed=�#��[ $��Atvienot��$	��Top=�$
��Left=��� -   $��Reģistrēties��	 � Cursor$
	 Hand��$��Bottom=�$��Right=�$��0,0,4,4q�$��12(�9� -   $��expFilebase����� .+�� -   $��progressBarFilebase��#��[  $y�Hidden=� . $��	-15,0,0,0q�]� .+��� $	��4,0q�$��16��$4��./TM2;component/Images/Trackers/filebase.ws.png���� $	��1,0q�$��Filebase.ws��$
��Bold"�$��Center=��� -   $��imgFilebaseYesOrNo��$��16��$	��4,0q�$-��'/TM2;component/Images/Login/hand_no.png��$��Right=�$��Right=���]� $��	0,0,0,-15q�.+��� $��Lietotājvārds��$	��Top=��� -   $��txtFilebase��#��[ $	��Top=�$	��200��$��30��$
��Left=��� $��Parole��$	��Top=�G� -   $��
pwFilebase��$	��Top=�$	��200��$��30��$
��Left=��� -   $��txtFilebaseError��$��12(�
���  ��$	��Top=�$��0,2,0,-2q��� -   $��btnFilebase��#��[ $��Pievienoties��$	��Top=�$
��Left=��� -   $��btnFilebaseDisconnect��$y�	Collapsed=�#��[ $��Atvienot��$	��Top=�$
��Left=��� -   $��txtRegFilebase��$��Reģistrēties��$
	 Hand��$��Bottom=�$��Right=�$��0,0,4,4q�$��12(�