   M S B A M L   `   `   `  ����L  Hpeersplit-desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:peersplit_desktoppeersplit_desktop  5   	   "    peersplit_desktop.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:peersplit_desktop     5   	     � Title$  	Peersplit��5   	   $	��450��6   $	��411��6(    � 
ResizeMode$ CanMinimize=�64   .��5
      � $	��419��6   $	��Top=�6   $��0,0,0,0q�60   .+�5   
   �� -   5         ��Name$ main_username_label��5      $��-----��6-   $
��Left=�6=   $��	10,40,0,0q�6X   $	��Top=�6k   $��
SF UI Text)�6�   $��14(�6�   �� 5   
   -   5        $ main_email_label��5      $��--@--��6*   $
��Left=�6:   $��	10,58,0,0q�6U   $	��Top=�6h   $��
SF UI Text)�6�   $��14(�6�   �� 5   
   -   5        $ main_storage_label��5      $��--/--��6,   $
��Left=�6<   $��	10,76,0,0q�6W   $	��Top=�6j   $��
SF UI Text)�6�   $��14(�6�   �� 5   
   $��Account Details��6   $
��Left=�6*   $��	10,10,0,0q�6E   $	��Top=�6X   $��18(�6p   $��
SF UI Text)�6~   �� 5   
   $
��Left=�6   $��5��6/   $��	10,34,0,0q�6:   $	��Top=�6M   $	��191��6e   �� 5   
   -   5         ��Name$ main_files_listView��5      $
��Left=�60   $	��399��6K   $��
206,10,0,0q�6X   $	��Top=�6l   $	��187��6�   $��Center=�6�    �� View 5      �� 5      .P�5      �� $��Filename��6%    �� Width$	 100��67    �� DisplayMemberBinding 6C   �� *
filename+�� 5      $��	Size (MB)��6%   $ 77��68    6C   �� *size+5      5      �� 5   
   $
��Left=�6   $��5��6/   $��
10,148,0,0q�6:   $	��Top=�6N   $	��191��6f   �� 5   
   $��File Manager��6   $
��Left=�6'   $��
10,124,0,0q�6B   $	��Top=�6V   $��18(�6n   $��
SF UI Text)�6|   �� 5   
   -   5        $��Select File��5      $
��Left=�6'   $��
10,160,0,0q�6B   $	��Top=�6V   $��75��6n   6y   � 5   
   -   5         �Name$ main_filemanager_pane��5      $
��Left=�6.   $��85��6I   $��
10,185,0,0q�6U   $	��Top=�6i   $	��191��6�   .+�5      �� $��	Filename:��6   $
��Left=�6(   $	��Top=�6C   �� 5      -   5        $ main_filename_label��5      $��Label��61   $
��Left=�6A   $��60,0,0,0q�6\   $	��Top=�6n   $	��131��6�   �� 5      $��Size:��6   $
��Left=�6$   $	��Top=�6?   $��0,26,0,0q�6W   $��38��6i   �� 5      -   5        $ main_size_label��5      $��Label��6-   $
��Left=�6=   $��	60,26,0,0q�6X   $	��Top=�6k   $	��131��6�   �� 5       -	   5         ��Name$ main_upload_button��5       $��Upload��61   $
��Left=�6B   $��	55,57,0,0q�6]   $	��Top=�6p   $��75��6�   6�   5!      �� 5"   
   $
��Left=�6   $��5��6/   $��
10,295,0,0q�6:   $	��Top=�6N   $	��191��6f   �� 5#   
   $��Settings��6   $
��Left=�6#   $��
10,270,0,0q�6>   $	��Top=�6R   $��18(�6j   $��
SF UI Text)�6x   �� 5$   
   -
   5        $ main_uploadMSG_label��5$      $��Label��6.   $
��Left=�6>   $��
90,158,0,0q�6Y   $	��Top=�6m   	 J� RenderTransformOrigin$	 -0.463,0.054&�6�   $	��111��6�   $��27��6�   �� 5%   
   $
��Left=�6   $��
10,306,0,0q�6+   $	��Top=�6?   �� 5&   
   -   5        
 ��Name$
 main_allowStorage_check��5&      $��Allow storage on device��64   $
��Left=�6V   $��
10,305,0,0q�6q   $	��Top=�6�   �� 5'   
   -   5         ��Name$ main_storage_amount��5'      $
��Left=�6/   $��23��6J   $��
10,325,0,0q�6V    �� TextWrapping$
 Wrap=�6j   $��20��6~   $	��Top=�6�   $��26��6�   $��Center=�6�   $��Center=�6�   �� 5(   
   $��GB Allocation��6   $
��Left=�6(   $��
35,324,0,0q�6C   $	��Top=�6W   �� 5)   
   -   5        $ main_activate_button��5)      $��Activate��6/   $
��Left=�6B   $��
62,362,0,0q�6]   $	��Top=�6q   $��75��6�   $	 -0.027,0.23&�6�   6�   5*      5+      