   M S B A M L   `   `   `  ����L  Hpeersplit-desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:peersplit_desktoppeersplit_desktop  5   	   #    peersplit_desktop.LoginWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:peersplit_desktop     5   	     � Title$  Peersplit - Login��5   	   $	��500��6#   $	��421��60    � Topmost .  � TaskbarItemInfo 5	      *  $System.Windows.Shell.TaskbarItemInfo  5
   
   .��5      � $	��470��6   $	��Top=�6   $��0,0,0,0q�60   -  'System.Windows.Input.KeyboardNavigation   TabNavigation$ Cycle=�6A    � RowDefinitions 5   
   �� 5      �� 5      $��0* �6   .+�5   
   �� -   5         ��Name$ login_username_input��5      $��Center=�60   $��45��6M   $��61,122,61,0q�6Y    �� TextWrapping$
 Wrap=�6n   $	��Top=�6�   $	��270��6�   $��16(�6�   $��Center=�6�   $��Center=�6�   G� 5   
   -   5         G�Name$ login_password_input��5      $��Center=�64   $��	0,203,0,0q�6Q   $	��Top=�6d   $	��270��6|   $��45��6�   $��16(�6�   $��Center=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ login_login_button��5      $��Login��6.   $��0,276,0,134q�6>   $��Center=�6S   $	��174��6n   $��60��6�   $��Center=�6�   �� 5   
   $&�� Login to your Peersplit account.��6   $��Center=�6;   $��
78,40,78,0q�6X   $	��Top=�6l   	 J� RenderTransformOrigin$	 -0.316,-0.231&�6�   $	��236��6�   $
��Bold"�6�   $��
SF UI Text)�6�   $��14(�6�   $��27��6�   �� 5   
   $��Username��6   $��Center=�6#   $��158,96,158,0q�6@   $	��Top=�6V   $��26��6n   $��76��6z   $��14(�6�   �� 5   
   $��Password��6   $��Center=�6#   $��159,172,159,0q�6@   $	��Top=�6W   $��26��6o   $��74��6{   $��14(�6�   �� 5   
   -   5        
 ��Name$
 login_invalidText_label��5      $.��(Invalid username or password! Try again!��61   $��Center=�6d   $��109,243,111,0q�6�   $	��Top=�6�   
���  ��$y�Hidden=�6�   5      5      