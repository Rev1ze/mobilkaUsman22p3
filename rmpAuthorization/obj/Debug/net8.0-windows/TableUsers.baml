   M S B A M L   `   `   `  ����K  GrmpAuthorization, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:rmpAuthorizationrmpAuthorization  5   	   !    rmpAuthorization.TableUsers   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   A =Wpf.Ui, Version=3.0.5.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:rmpAuthorization     5   	     � Title$  
TableUsers��5	   	   $	��450��5
      $	��800��6    � 	Resources 5       �� 
TargetType �� Property �� Value��%U   '��       �� 5   
   " ��6   .�5      ��  � {�$ False��6*   5      5      .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63    �� RelativeTransform 5      V� 5      .~�5      ��  �� CenterY$	 0.5Q�6)   	 �� CenterX$		 0.5Q�67   �� 5      
 �� CenterX$	
 0.5Q�6(    �� CenterY$	 0.5Q�66   �� 5       �� Angle$ 243.55800000000002Q�6*    �� CenterX$	 0.5Q�6E    �� CenterY$	 0.5Q�6S   T� 5      5      .��5      �  � Color$ 	#FFE41DFC��6   � 5      $ 	#FFF78F13��6    � Offset$ 1Q�61   5       � RowDefinitions 5   
   �� 5       $��40 �6   �� 5!      $��* �6   �� 5"      $��40 �6   .+�5&   
     Wpf.Ui.Controls.TitleBar  -   5           Title$% Все пользователи��5&      $��0��6T     Wpf.Ui.Controls.Button  5'   
   -   5        $��Обновить��5'      $��2��6'   $��Center=�6P   $��Center=�6m     Wpf.Ui.Controls.DataGrid  5(   
   -   5          Name$ dg��5(   J   &   System.Windows.Controls.DataGrid   AutoGenerateColumns$
 True��6   $��1��61   $��20q�6>   5)      5,      5-      