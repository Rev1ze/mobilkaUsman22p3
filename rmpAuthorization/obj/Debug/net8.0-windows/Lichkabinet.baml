   M S B A M L   `   `   `  ����K  GrmpAuthorization, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:rmpAuthorizationrmpAuthorization  5   	   "    rmpAuthorization.Lichkabinet   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   A =Wpf.Ui, Version=3.0.5.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   *localclr-namespace:rmpAuthorization  5   	     � Title$  Lichkabinet��5	   	   $	��600��6   $	��800��6*   .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63    �� RelativeTransform 5      V� 5      .~�5      ��  �� CenterY$	 0.5Q�6)    �� CenterX$	 0.5Q�67   �� 5       �� CenterX$	 0.5Q�6(    �� CenterY$	 0.5Q�66   �� 5       �� Angle$ 243.55800000000002Q�6*   	 �� CenterX$		 0.5Q�6E   
 �� CenterY$	
 0.5Q�6S   T� 5      5      .��5      �  � Color$ 	#FFE41DFC��6   � 5      $ 	#FFF78F13��6    � Offset$ 1Q�61   5       � RowDefinitions 5   
   �� 5      $	��25* �6   �� 5      $
��192* �6   .+�5    
     Wpf.Ui.Controls.TitleBar  -   5           Title$! Личный кабинет��5       6-   �� 5"   
   $	��350��6   $
��Left=�6$   $��1��6?   .+�5$      �� $'��!Электронная почта��6     Wpf.Ui.Controls.TextBox  5%      -   5          Name$ 	tboxGmail��5%       �� 	MaxLength$ 50��6�   
���   �6�   �� 5&      $��
Логин��6     5'      -   5        $ 	tboxLogin��5'      
���   �$ 50��6�   �� 5)      $��Пароль��6   !  Wpf.Ui.Controls.PasswordBox  5*      -   5          Name$ tboxPassword��5*      
���   �$ 50��6�   �� 5,      $��Новый пароль��6     5-      -   5        $ tboxPasswordNew��5-      
���   �$ 50��6�   �� 5/      $��Имя��6     50      -   5        $ tboxName��50      
���   �$ 50��6_    � ContextMenu 6�   Z� 6�   �� 51      $��Фамилия��6     52      -   5        $ tboxSurname��52      
���   �$ 50��6b   6�   �� 54      $��Отчество��6     55      -   5        $ tboxMidName��55      $ 50��6N   
���   �6�   �� 56      6     Wpf.Ui.Controls.Button  57      -	   5        $��Center=�57      $#��Поменять данные��65   6l   58      �� 59   
   -
   5         ��Name$ stackPanelProblems��59      $��Right=�62   $��Bottom=�6N   $	��400��6i   $	��500��6x   $��1��6�   5;      5<      