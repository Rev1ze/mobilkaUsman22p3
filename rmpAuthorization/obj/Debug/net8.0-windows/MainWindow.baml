   M S B A M L   `   `   `  ����K  GrmpAuthorization, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:rmpAuthorizationrmpAuthorization  5   	   !    rmpAuthorization.MainWindow   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   A =Wpf.Ui, Version=3.0.5.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   *localclr-namespace:rmpAuthorization  5   	     � WindowStartupLocation$  CenterScreen=�5       � Title$ Авторизация��5   	   $	��366��6   $	��404��6*   $	��277��66    � 	Resources 5       �� 
TargetType �� Property �� Value��%T   '��       �� 5   
   " ��6   .�5      ��  � ��
 �   �6+   5      5      .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63    �� RelativeTransform 5      V� 5      .~�5      �� 	 �� CenterY$		 0.5Q�6)   
 �� CenterX$	
 0.5Q�67   �� 5       �� CenterX$	 0.5Q�6(    �� CenterY$	 0.5Q�66   �� 5       �� Angle$ 243.55800000000002Q�6*    �� CenterX$	 0.5Q�6E    �� CenterY$	 0.5Q�6S   T� 5      5      .��5      �  � Color$ 	#FFE41DFC��6   � 5      $ 	#FFF78F13��6    � Offset$ 1Q�61   5      .+�5    
   �� -   5        $	��123��5       $
��Left=�6   $��Bottom=�6:   6l   �� 5!   
   $	��Top=�6   .+�5"        Wpf.Ui.Controls.TitleBar     Title$ Авторизация��6      CanMaximize$
 True��6.   �� 5#      $#��Ваш логин(почта)��6   $��Center=�6/     Wpf.Ui.Controls.TextBox  5$      -   5          Name$ 	tboxLogin��5$       �� 	MaxLength$ 50��6,   
���   �6;   �� 5%      $��Ваш пароль��6   $��Center=�6)   !  Wpf.Ui.Controls.PasswordBox  5&      -   5          Name$ tboxPassword��5&      $ 50��63   
���   �6C   �� 5'      �� 5(        Wpf.Ui.Controls.Button  5)      -   5        $��
Войти��5)      $��Center=�6(   
���   �
���   �$	��230��6l   6x   �� 5*       �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5+      �� $��Нет аккаунта?��6   $��Center=�60   %  Wpf.Ui.Controls.HyperlinkButton  5,      -   5        $*��$Зарегистрироваться��5,   %   $��Center=�6B   $	��150��6_   6l   5-        5.      -   5        $��Забыл пароль��5.   !   $��Center=�68   6U   50      51      52      