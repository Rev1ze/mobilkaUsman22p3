   M S B A M L   `   `   `  ����K  GrmpAuthorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:rmpAuthorizationrmpAuthorization  5   	   !    rmpAuthorization.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   A =Wpf.Ui, Version=3.0.5.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   *localclr-namespace:rmpAuthorization  5   	     � WindowStartupLocation$  CenterScreen=�5       � Title$ Авторизация��5   	   $	��366��6   $	��404��6*   $	��277��66    � 	Resources 5       �� 
TargetType �� Property �� Value��%T   '��       �� 5   
   " ��6   .�5      ��  � ��
 �   �6+   5      5      .��5      � .+�5   
   �� -   5        $	��123��5      $
��Left=�6   $��Bottom=�6:   6l   �� 5   
   $	��Top=�6   .+�5        Wpf.Ui.Controls.TitleBar     Title$ Авторизация��6      CanMaximize . 6.   �� 5      $#��Ваш логин(почта)��6   $��Center=�6/     Wpf.Ui.Controls.TextBox  5      -   5          Name$ 	tboxLogin��5      	 �� 	MaxLength$	 50��6,   
���   �6;   �� 5      $��Ваш пароль��6   $��Center=�6)     5      -   5        $ tboxPassword��5      $	 50��6/   
���   �6?   �� 5      �� 5        Wpf.Ui.Controls.Button  5      -   5        $��
Войти��5      $��Center=�6(   
���   �
���   �$	��230��6l   6x   �� 5      
 �� Orientation$
 
Horizontal=�6   $��Center=�62   .+�5      �� $��Нет аккаунта?��6   $��Center=�60   %  Wpf.Ui.Controls.HyperlinkButton  5      -   5        $*��$Зарегистрироваться��5   %   $��Center=�6B   $	��150��6_   6l   5        5       -   5        $��Забыл пароль��5    !   $��Center=�68   6U   5"      5#      5$      