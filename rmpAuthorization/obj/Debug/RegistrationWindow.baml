   M S B A M L   `   `   `  ����K  GrmpAuthorization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:rmpAuthorizationrmpAuthorization  5   	   )    #rmpAuthorization.RegistrationWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   A =Wpf.Ui, Version=3.0.5.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5   	   *localclr-namespace:rmpAuthorization  5   	     � WindowStartupLocation$  CenterScreen=�5	       � Title$ Регистрация��5   	   $	��617��6   $	��575��6*   $	��381��66   $	��617��6E   $	��617��6U    � 	Resources 5       �� 
TargetType �� Property �� Value��%T   '��       �� 5   
   " ��6   .�5      ��  � ��
 �   �6+   5      5      .��5      � $��0,0,0,-6q�6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $	��35* �6   .+�5   
     Wpf.Ui.Controls.TitleBar  
���   �   Title$ Регистрация��6)      CanMaximize . $��0��6P   �� 5   
   $��	5,0,257,0q�6   $��1��6(   .+�5      �� $'��!Электронная почта��6     Wpf.Ui.Controls.TextBox  5      -   5        	  Name$	 	tboxGmail��5      
 �� 	MaxLength$
 50��6�   
���   � �� TextWrapping$ NoWrap=�6�   �� 5      $��
Логин��6     5      -   5        $	 	tboxLogin��5      
���   �$
 50��6   �� 5      $��Пароль��6   !  Wpf.Ui.Controls.PasswordBox  5      -   5          Name$ tboxPassword��5      
���   �$
 50��6�   �� 5!      $'��!Потвердите пароль��6     5"      -   5        $ tboxPasswordConfirm��5"      $
 50��6y   
���   �6�   �� 5$      $��Имя��6     5%      -   5        $	 tboxName��5%      
���   �$
 50��6?    � ContextMenu 6�   Z� 6�   �� 5&      $��Фамилия��6     5'      -   5        $	 tboxSurname��5'      
���   �$
 50��6B   6�   �� 5)      $*��$Отчество (если есть)��6     5*      -   5        $	 tboxMidName��5*      $
 50��6.   
���   �6�   �� 5,       �� Orientation$ 
Horizontal=�6   $��Center=�62   $��Center=�6M   .+�5-      �� $��Дата рождения��6   (  "Wpf.Ui.Controls.CalendarDatePicker  5.      -   5          Name$ calendar��5.   (   
���   �$��Center=�6N   $��Center=�6i   $��90��6�   5/      �� 50      6     Wpf.Ui.Controls.Button  51      -	   5        $*��$Зарегистрироваться��51      
���   �
���   �$��Center=�6`   $	��180��6}   6�   �� 52      6     53      -
   5        $.��(Обратно к авторизации��53      
���   �
���   �$��Center=�6_   $	��180��6|   6�   54      �� 55   
   -   5         ��Name$ stackPanelProblems��55      $��Right=�62   $��Bottom=�6N   $	��383��6i   $��2��6v   57      58      