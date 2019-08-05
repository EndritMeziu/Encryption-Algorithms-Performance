#! /usr/bin/env python
#  -*- coding: utf-8 -*-
#
# GUI module generated by PAGE version 4.24.1
#  in conjunction with Tcl version 8.6
#    Aug 01, 2019 04:00:12 PM CEST  platform: Windows NT
import sys
import os
import random
import time

import matplotlib.pyplot as plt
import Cryptodome.Cipher.DES as DES
import Cryptodome.Cipher.AES as AES
import Cryptodome.Cipher.ARC4 as RC4
import Cryptodome.Cipher.DES3 as DES3
import Cryptodome.Cipher.Blowfish as Blowfish
from Cryptodome.Util.Padding import pad, unpad
from matplotlib.figure import Figure
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg, NavigationToolbar2Tk
from tkinter import *
from PIL import Image as Oimage, ImageTk

values = [0, 0, 0, 0, 0]
labels = ["EncrypTime", "DecrypTime", "KeyGeneration", "EncryptLoad", "DecryptLoad"]

encval = [0,0,0,0,0]
decval = [0,0,0,0,0]
keygenval = [0,0,0,0,0]
algLabels = ["AES","DES","3DES","Blowfish","RC4"]
global photo
try:
    import Tkinter as tk

    plt.use('TkAgg')
except ImportError:
    import tkinter as tk

try:
    import ttk

    py3 = False
except ImportError:
    import tkinter.ttk as ttk

    py3 = True

import EncryptionAlgorithmsPerformance_gui_support


def vp_start_gui():
    '''Starting point when module is the main routine.'''
    global val, w, root
    root = tk.Tk()

    canvas = tk.Canvas(root)
    EncryptionAlgorithmsPerformance_gui_support.set_Tk_var()
    top = Toplevel1(root)
    EncryptionAlgorithmsPerformance_gui_support.init(root, top)
    root.mainloop()


w = None


def create_Toplevel1(root, *args, **kwargs):
    '''Starting point when module is imported by another program.'''
    global w, w_win, rt
    rt = root
    w = tk.Toplevel(root)
    EncryptionAlgorithmsPerformance_gui_support.set_Tk_var()
    top = Toplevel1(w)
    EncryptionAlgorithmsPerformance_gui_support.init(w, top, *args, **kwargs)
    return (w, top)


def destroy_Toplevel1():
    global w
    w.destroy()
    w = None


class Toplevel1:
    def generate_File(self, event):
        letters = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
                   's', 't', 'u', 'v', 'w', 'x', 'y', 'z', " "]
        if (self.cmbFile.get() == "1Kb"):
            f = open("textfile.txt", "w+")
            for i in range(1024 - 114):
                f.write(letters.__getitem__(random.randint(0, 25)))
                if (i % 8 == 0):
                    f.write(letters.__getitem__(26))
        elif (self.cmbFile.get() == "10Kb"):
            f = open("textfile.txt", "w+")
            for i in range((1024 - 114) * 10):
                f.write(letters.__getitem__(random.randint(0, 25)))
                if (i % 8 == 0):
                    f.write(letters.__getitem__(26))
        elif (self.cmbFile.get() == "100Kb"):
            f = open("textfile.txt", "w+")
            for i in range((1024 - 114) * 100):
                f.write(letters.__getitem__(random.randint(0, 25)))
                if (i % 8 == 0):
                    f.write(letters.__getitem__(26))
        elif (self.cmbFile.get() == "1000Kb"):
            f = open("textfile.txt", "w+")
            for i in range((1024 - 114) * 1000):
                f.write(letters.__getitem__(random.randint(0, 25)))
                if (i % 8 == 0):
                    f.write(letters.__getitem__(26))

    def generate_Key(self):
        size = int(int(self.cmbKeySize.get()))
        key = ""
        letters = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
                   's', 't', 'u', 'v', 'w', 'x', 'y', 'z']
        for i in range(50):
            key = ""
            for i in range(int(size / 8)):
                key += letters.__getitem__(random.randint(0, 25))

        return key


    def display_Graph(self,event):
        paramLabels = ["","","","",""]


        if(self.cmbResultType.get() == "DecryptTime"):
            children = list(self.Frame4_6.children.values())
            for child in children:
                child.destroy()
            fig2 = plt.Figure()
            canvas2 = FigureCanvasTkAgg(fig2, self.Frame4_6)
            canvas2.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax2 = fig2.add_subplot(111)

            for i in range(5):
                paramLabels[i] += algLabels[i]+" " + str(decval[i]) + "μs"

            ax2.pie(decval)
            ax2.legend(paramLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

        elif(self.cmbResultType.get() == "KeyGeneration"):
            children = list(self.Frame4_6.children.values())
            for child in children:
                child.destroy()
            fig2 = plt.Figure()
            canvas2 = FigureCanvasTkAgg(fig2, self.Frame4_6)
            canvas2.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax2 = fig2.add_subplot(111)

            for i in range(5):
                paramLabels[i] += algLabels[i]+" " + str(keygenval[i]) + "μs"

            ax2.pie(keygenval)
            ax2.legend(paramLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

    def display_Results(self, event):

        paramLabels = ["","","","",""]
        algParamLabels = ["","","","",""]

        if (self.cmbAlgorithm.get() == "AES"):

            encsum = 0
            decsum = 0
            children = list(self.Frame4.children.values())
            for child in children:
                child.destroy()

            children2 = list(self.Frame4_6.children.values())
            for child in children2:
                child.destroy()

            self.cmbResultType.current(0)
            timekey1 = time.time()
            # key generation
            for i in range(10000):
                key = os.urandom(int(int(self.cmbKeySize.get()) / 8))
            timekey2 = time.time()
            values[2] = ((timekey2 - timekey1) * 1000).__format__('.2f')

            # encryption
            in_file = open("textfile.txt", "rb")  # opening for [r]eading as [b]inary
            data = in_file.read()  # if you only wanted to read 512 bytes, do .read(512)
            in_file.close()

            aes = AES.new(key, AES.MODE_ECB)
            for i in range(10):
                time1 = time.time()
                for i in range(150):
                    encrypteddata = aes.encrypt(pad(data, AES.block_size))
                time2 = time.time()
                encsum += ((time2 * 1000000 - time1 * 1000000))


            out_file = open("encryptedfile.txt", "wb")
            out_file.write(encrypteddata)
            values[0] = (encsum / 10.0).__format__('.2f')

            # decryption

            for i in range(10):
                timedec1 = time.time()
                for i in range(150):
                    decrypteddata = aes.encrypt(encrypteddata)
                timedec2 = time.time()
                decsum += ((timedec2 * 1000000 - timedec1 * 1000000))
            out_file.close()
            out_file = open("decryptedfile.txt", "wb")
            out_file.write(decrypteddata)
            out_file.close()

            values[1] = (decsum / 10.0).__format__('.2f')

            #First graph
            fig = plt.Figure()
            canvas = FigureCanvasTkAgg(fig, self.Frame4)
            canvas.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax = fig.add_subplot(111)

            for i in range(5):
                paramLabels[i] += labels[i] + " " + str(values[i]) + "μs"

            ax.pie(values)
            ax.legend(paramLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

            encval[0] = values[0]
            decval[0] = values[1]
            keygenval[0] = values[2]
            #Second graph
            fig2 = plt.Figure()
            canvas2 = FigureCanvasTkAgg(fig2, self.Frame4_6)
            canvas2.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax2 = fig2.add_subplot(111)

            for i in range(5):
                algParamLabels[i] += algLabels[i]+" " + str(encval[i]) + "μs"

            ax2.pie(encval)
            ax2.legend(algParamLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

        elif (self.cmbAlgorithm.get() == "DES"):
            encsum = 0
            decsum = 0
            time1key = time.time()
            for i in range(3000):
                key = os.urandom(8)
            time2key = time.time()

            values[2] = ((time2key * 1000000 - time1key * 1000000)).__format__('.2f')
            # Encryption
            in_file = open("textfile.txt", "rb")  # opening for [r]eading as [b]inary
            data = in_file.read()  # if you only wanted to read 512 bytes, do .read(512)
            in_file.close()
            des = DES.new(key, DES.MODE_ECB)
            for i in range(5):
                time1enc = time.time()
                for i in range(100):
                    encrypteddata = des.encrypt(pad(data, DES.block_size))
                time2enc = time.time()
                encsum += ((time2enc * 1000000 - time1enc * 1000000))
            out_file = open("encryptedfile.txt", "wb")
            out_file.write(encrypteddata)

            values[0] = (encsum / 5.0).__format__('.2f')

            for i in range(5):
                time1dec = time.time()
                for i in range(100):
                    decrypteddata = des.decrypt(encrypteddata)
                time2dec = time.time()
                decsum += ((time2dec * 1000000 - time1dec * 1000000))

            out_file.close()
            out_file = open("decryptedfile.txt", "wb")
            out_file.write(decrypteddata)
            out_file.close()
            values[1] = (decsum / 5.0).__format__('.2f')

            children = list(self.Frame4.children.values())
            for child in children:
                child.destroy()

            children2 = list(self.Frame4_6.children.values())
            for child in children2:
                child.destroy()

            self.cmbResultType.current(0)

            fig = plt.Figure()
            canvas = FigureCanvasTkAgg(fig, self.Frame4)
            canvas.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax = fig.add_subplot(111)

            for i in range(5):
                paramLabels[i] += labels[i] + " " + str(values[i]) + "μs"

            ax.pie(values)
            ax.legend(paramLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))


            encval[1] = values[0]
            decval[1] = values[1]
            keygenval[1] = values[2]
            # Second graph
            fig2 = plt.Figure()
            canvas2 = FigureCanvasTkAgg(fig2, self.Frame4_6)
            canvas2.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax2 = fig2.add_subplot(111)

            for i in range(5):
                algParamLabels[i] += algLabels[i]+" " + str(encval[i]) + "μs"

            ax2.pie(encval)
            ax2.legend(algParamLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

        elif (self.cmbAlgorithm.get() == "3DES"):
            encsum = 0
            decsum = 0
            children = list(self.Frame4.children.values())
            for child in children:
                child.destroy()

            children2 = list(self.Frame4_6.children.values())
            for child in children2:
                child.destroy()

            self.cmbResultType.current(0)

            time1key = time.time()
            key = os.urandom(int(int(self.cmbKeySize.get()) / 8))
            time2key = time.time()
            values[2] = ((time2key * 1000000 - time1key * 1000000)).__format__('.2f')
            # Encryption
            in_file = open("textfile.txt", "rb")  # opening for [r]eading as [b]inary
            data = in_file.read()  # if you only wanted to read 512 bytes, do .read(512)

            des3 = DES3.new(key, DES3.MODE_ECB)
            for i in range(10):
                time1enc = time.time()
                for i in range(250):
                    encrypteddata = des3.encrypt(pad(data, DES3.block_size))
                time2enc = time.time()
                encsum += ((time2enc * 1000000 - time1enc * 1000000))
            values[0] = (encsum / 10.0).__format__('.2f')
            out_file = open("encryptedfile.txt", "wb")
            out_file.write(encrypteddata)

            # Decryption
            for i in range(10):
                time1dec = time.time()
                for i in range(250):
                    decrypteddata = des3.decrypt(encrypteddata)
                time2dec = time.time()
                decsum += ((time2dec * 1000000 - time1dec * 1000000))
            out_file.close()
            out_file = open("decryptedfile.txt", "wb")
            out_file.write(decrypteddata)
            out_file.close()
            values[1] = (decsum / 10.0).__format__('.2f')

            fig = plt.Figure()
            canvas = FigureCanvasTkAgg(fig, self.Frame4)
            canvas.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)

            ax = fig.add_subplot(111)

            for i in range(5):
                paramLabels[i] += labels[i] + " " + str(values[i]) + "μs"

            ax.pie(values)
            ax.legend(paramLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

            encval[2] = values[0]
            decval[2] = values[1]
            keygenval[2] = values[2]
            # Second graph
            fig2 = plt.Figure()
            canvas2 = FigureCanvasTkAgg(fig2, self.Frame4_6)
            canvas2.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax2 = fig2.add_subplot(111)

            for i in range(5):
                algParamLabels[i] += algLabels[i]+" " + str(encval[i]) + "μs"

            ax2.pie(encval)
            ax2.legend(algParamLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

        elif (self.cmbAlgorithm.get() == "Blowfish"):
            encsum = 0
            decsum = 0
            children = list(self.Frame4.children.values())
            for child in children:
                child.destroy()

            children2 = list(self.Frame4_6.children.values())
            for child in children2:
                child.destroy()


            self.cmbResultType.current(0)

            time1key = time.time()
            for i in range(100):
                key = os.urandom(int(int(self.cmbKeySize.get()) / 8))
            time2key = time.time()
            values[2] = ((time2key - time1key) * 1000000).__format__('.2f')
            # Encryption

            in_file = open("textfile.txt", "rb")  # opening for [r]eading as [b]inary
            data = in_file.read()  # if you only wanted to read 512 bytes, do .read(512)
            time1enc = time.time()
            blowfish = Blowfish.new(key, Blowfish.MODE_ECB)
            for i in range(10):
                time1enc = time.time()
                for i in range(250):
                    encrypteddata = blowfish.encrypt(pad(data, Blowfish.block_size))
                time2enc = time.time()
                encsum += ((time2enc * 1000000 - time1enc * 1000000))
            out_file = open("encryptedfile.txt", "wb")
            out_file.write(encrypteddata)
            out_file.close()

            values[0] = (encsum / 10.0).__format__('.2f')

            # decryption

            in_file = open("encryptedfile.txt", "rb")  # opening for [r]eading as [b]inary
            data = in_file.read()  # if you only wanted to read 512 bytes, do .read(512)
            for i in range(10):
                time1dec = time.time()
                for i in range(250):
                    decrypteddata = blowfish.decrypt(encrypteddata)
                time2dec = time.time()
                decsum += ((time2dec * 1000000 - time1dec * 1000000))
            out_file = open("decryptedfile.txt", "wb")
            out_file.write(decrypteddata)
            out_file.close()

            values[1] = (decsum / 10.0).__format__('.2f')

            fig = plt.Figure()
            canvas = FigureCanvasTkAgg(fig, self.Frame4)
            canvas.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax = fig.add_subplot(111)

            for i in range(5):
                paramLabels[i] += labels[i] + " " + str(values[i]) + "μs"

            ax.pie(values)
            ax.legend(paramLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

            encval[3] = values[0]
            decval[3] = values[1]
            keygenval[3] = values[2]
            # Second graph
            fig2 = plt.Figure()
            canvas2 = FigureCanvasTkAgg(fig2, self.Frame4_6)
            canvas2.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax2 = fig2.add_subplot(111)

            for i in range(5):
                algParamLabels[i] += algLabels[i]+" " + str(encval[i]) + "μs"

            ax2.pie(encval)
            ax2.legend(algParamLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

        elif (self.cmbAlgorithm.get() == "RC4"):
            encsum = 0
            decsum = 0
            children = list(self.Frame4.children.values())
            for child in children:
                child.destroy()

            children2 = list(self.Frame4_6.children.values())
            for child in children2:
                child.destroy()

            self.cmbResultType.current(0)

            time1key = time.time()
            for i in range(5000):
                key = os.urandom(int(int(self.cmbKeySize.get()) / 8))
            time2key = time.time()
            values[2] = ((time2key - time1key) * 1000000).__format__('.2f')
            # Encryption
            in_file = open("textfile.txt", "rb")  # opening for [r]eading as [b]inary
            data = in_file.read()  # if you only wanted to read 512 bytes, do .read(512)
            rc4 = RC4.new(key, 0)
            for i in range(10):
                time1enc = time.time()
                for i in range(250):
                    encrypteddata = rc4.encrypt(pad(data, RC4.block_size))
                time2enc = time.time()
                encsum += ((time2enc * 1000000 - time1enc * 1000000))
            out_file = open("encryptedfile.txt", "wb")
            out_file.write(encrypteddata)

            values[0] = (encsum / 10.0).__format__('.2f')

            # decryption

            for i in range(10):
                time1dec = time.time()
                for i in range(250):
                    decrypteddata = rc4.decrypt(encrypteddata)
                time2dec = time.time()
                decsum += ((time2dec * 1000000 - time1dec * 1000000))
            out_file = open("decryptedfile.txt", "wb")
            out_file.write(decrypteddata)
            out_file.close()

            values[1] = (decsum / 10.0).__format__('.2f')

            fig = plt.Figure()
            canvas = FigureCanvasTkAgg(fig, self.Frame4)
            canvas.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax = fig.add_subplot(111)

            for i in range(5):
                paramLabels[i] += labels[i] + " " + str(values[i]) + "μs"

            ax.pie(values)
            ax.legend(paramLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

            encval[4] = values[0]
            decval[4] = values[1]
            keygenval[4] = values[2]
            # Second graph
            fig2 = plt.Figure()
            canvas2 = FigureCanvasTkAgg(fig2, self.Frame4_6)
            canvas2.get_tk_widget().pack(side=tk.TOP, fill=tk.BOTH, expand=1)
            ax2 = fig2.add_subplot(111)

            for i in range(5):
                algParamLabels[i] += algLabels[i]+" " + str(encval[i]) + "μs"

            ax2.pie(encval)
            ax2.legend(algParamLabels, bbox_to_anchor=(0.4, 0.15, 0.1, 0.1))

    def set_keySize(self, event):
        if (self.cmbAlgorithm.get() == "AES"):
            self.cmbKeySize['values'] = ("128", "192", "256")
        elif (self.cmbAlgorithm.get() == "DES"):
            self.cmbKeySize['values'] = ("64")
        elif (self.cmbAlgorithm.get() == "3DES"):
            self.cmbKeySize['values'] = ("128", "192")


        elif (self.cmbAlgorithm.get() == "Blowfish"):
            self.cmbKeySize['values'] = ("32", "128", "256", "448")
        elif (self.cmbAlgorithm.get() == "RC4"):
            self.cmbKeySize['values'] = ("128", "192", "256")

    def __init__(self, top=None):
        '''This class configures and populates the toplevel window.
           top is the toplevel containing window.'''
        _bgcolor = '#d9d9d9'  # X11 color: 'gray85'
        _fgcolor = '#000000'  # X11 color: 'black'
        _compcolor = '#d9d9d9'  # X11 color: 'gray85'
        _ana1color = '#d9d9d9'  # X11 color: 'gray85'
        _ana2color = '#ececec'  # Closest X11 color: 'gray92'
        self.style = ttk.Style()
        if sys.platform == "win32":
            self.style.theme_use('winnative')
        self.style.configure('.', background=_bgcolor)
        self.style.configure('.', foreground=_fgcolor)
        self.style.configure('.', font="TkDefaultFont")
        self.style.map('.', background=
        [('selected', _compcolor), ('active', _ana2color)])

        top.geometry("1084x775+320+136")
        top.title("New Toplevel")
        top.configure(background="#f4f4f4")
        top.configure(highlightbackground="#FF0000")
        top.configure(highlightcolor="black")

        self.Frame1 = tk.Frame(top)
        self.Frame1.place(relx=0.0, rely=0.116, relheight=0.885, relwidth=0.183)
        self.Frame1.configure(relief='groove')
        self.Frame1.configure(borderwidth="2")
        self.Frame1.configure(relief="groove")
        self.Frame1.configure(background="#243a51")
        self.Frame1.configure(highlightbackground="#d9d9d9")
        self.Frame1.configure(width=105)

        img = Oimage.open('encrypttt.png')
        img = img.resize((120,110),Oimage.ANTIALIAS)
        self.photo = ImageTk.PhotoImage(img)

        self.Canvas1 = tk.Canvas(self.Frame1,bd=0,highlightthickness=0)
        self.Canvas1.place(relx=0.163, rely=0.119, relheight=0.170, relwidth = 0.850)
        self.Canvas1.configure(background="#243a51")
        self.Canvas1.configure(relief="ridge")
        self.Canvas1.configure(width=143)
        self.Canvas1.create_image(0,0,anchor=NW,image=self.photo)




        self.Frame2 = tk.Frame(top)
        self.Frame2.place(relx=0.0, rely=0.0, relheight=0.111, relwidth=1.0)
        self.Frame2.configure(relief='groove')
        self.Frame2.configure(borderwidth="2")
        self.Frame2.configure(relief="groove")
        self.Frame2.configure(background="#243a51")
        self.Frame2.configure(highlightbackground="#d9d9d9")
        self.Frame2.configure(width=125)

        self.Frame3 = tk.Frame(top)
        self.Frame3.place(relx=0.221, rely=0.194, relheight=0.221
                          , relwidth=0.225)
        self.Frame3.configure(relief='raised')
        self.Frame3.configure(borderwidth="2")
        self.Frame3.configure(relief="raised")
        self.Frame3.configure(background="#FFFFFF")
        self.Frame3.configure(highlightbackground="#d9d9d9")
        self.Frame3.configure(highlightthickness="2")
        self.Frame3.configure(width=125)

        self.TSeparator1 = ttk.Separator(self.Frame3)
        self.TSeparator1.place(relx=0.164, rely=0.702, relwidth=0.615)

        self.cmbFile = ttk.Combobox(self.Frame3)
        self.cmbFile.place(relx=0.164, rely=0.409, relheight=0.152
                           , relwidth=0.643)
        self.cmbFile['values'] = ("1Kb", "10Kb", "100Kb", "1000Kb")
        self.cmbFile.configure(takefocus="")
        self.cmbFile.bind("<<ComboboxSelected>>", self.generate_File)


        self.Frame3_3 = tk.Frame(top)
        self.Frame3_3.place(relx=0.747, rely=0.194, relheight=0.221
                            , relwidth=0.225)
        self.Frame3_3.configure(relief='raised')
        self.Frame3_3.configure(borderwidth="2")
        self.Frame3_3.configure(relief="raised")
        self.Frame3_3.configure(background="#FFFFFF")
        self.Frame3_3.configure(highlightbackground="#d9d9d9")
        self.Frame3_3.configure(highlightthickness="2")
        self.Frame3_3.configure(width=125)

        self.TSeparator1_10 = ttk.Separator(self.Frame3_3)
        self.TSeparator1_10.place(relx=0.205, rely=0.702, relwidth=0.615)

        self.cmbKeySize = ttk.Combobox(self.Frame3_3)
        self.cmbKeySize.place(relx=0.205, rely=0.409, relheight=0.152
                              , relwidth=0.643)
        self.cmbKeySize.configure(takefocus="")
        self.cmbKeySize.bind("<<ComboboxSelected>>", self.display_Results)

        self.Frame3_4 = tk.Frame(top)
        self.Frame3_4.place(relx=0.48, rely=0.194, relheight=0.221
                            , relwidth=0.225)
        self.Frame3_4.configure(relief='raised')
        self.Frame3_4.configure(borderwidth="2")
        self.Frame3_4.configure(relief="raised")
        self.Frame3_4.configure(background="#FFFFFF")
        self.Frame3_4.configure(highlightbackground="#d9d9d9")

        self.Frame3_4.configure(highlightthickness="2")
        self.Frame3_4.configure(width=125)

        self.TSeparator1_9 = ttk.Separator(self.Frame3_4)
        self.TSeparator1_9.place(relx=0.205, rely=0.702, relwidth=0.615)

        self.cmbAlgorithm = ttk.Combobox(self.Frame3_4)
        self.cmbAlgorithm.place(relx=0.246, rely=0.409, relheight=0.152
                                , relwidth=0.643)
        self.cmbAlgorithm.configure(takefocus="")
        self.cmbAlgorithm['values'] = ("AES", "DES", "3DES", "Blowfish", "RC4")

        self.cmbAlgorithm.bind("<<ComboboxSelected>>", self.set_keySize)

        # this is it

        self.Frame4 = tk.Frame(top)
        self.Frame4.place(relx=0.221, rely=0.452, relheight=0.515
                          , relwidth=0.364)
        self.Frame4.configure(relief='raised')
        self.Frame4.configure(borderwidth="2")
        self.Frame4.configure(relief="raised")
        self.Frame4.configure(background="#ffffff")
        self.Frame4.configure(highlightbackground="#d9d9d9")
        self.Frame4.configure(highlightcolor="#000000")
        self.Frame4.configure(highlightthickness="2")
        self.Frame4.configure(width=125)

        self.Frame4_5 = tk.Frame(self.Frame4)
        self.Frame4_5.place(relx=1.962, rely=1.015, relheight=1.0, relwidth=1.0)
        self.Frame4_5.configure(relief='raised')
        self.Frame4_5.configure(borderwidth="2")
        self.Frame4_5.configure(relief="raised")
        self.Frame4_5.configure(background="#FF0000")
        self.Frame4_5.configure(highlightbackground="#d9d9d9")
        self.Frame4_5.configure(highlightcolor="#000000")
        self.Frame4_5.configure(highlightthickness="2")
        self.Frame4_5.configure(width=125)

        self.Frame4_6 = tk.Frame(top)
        self.Frame4_6.place(relx=0.6, rely=0.452, relheight=0.515
                            , relwidth=0.364)
        self.Frame4_6.configure(relief='raised')
        self.Frame4_6.configure(borderwidth="2")
        self.Frame4_6.configure(relief="raised")
        self.Frame4_6.configure(background="#ffffff")
        self.Frame4_6.configure(highlightbackground="#d9d9d9")
        self.Frame4_6.configure(highlightthickness="2")
        self.Frame4_6.configure(width=125)

        self.Frame4_7 = tk.Frame(top)
        self.Frame4_7.place(relx=0.602, rely=0.456, relheight=0.075
                            , relwidth=0.354)

        self.Frame4_7.configure(borderwidth="0")
        self.Frame4_7.configure(relief="raised")
        self.Frame4_7.configure(background="#FFFFFF")
        self.Frame4_7.configure(width=125)

        self.cmbResultType = ttk.Combobox(self.Frame4_7)
        self.cmbResultType.place(relx=0.305, rely=0.200, relheight=0.485
                              , relwidth=0.375)
        self.cmbResultType.configure(takefocus="")
        self.cmbResultType.bind("<<ComboboxSelected>>", self.display_Graph)
        self.cmbResultType['values'] = ("EncryptTime","DecryptTime","KeyGeneration")
        self.cmbResultType.current(0)
        self.Frame4_1 = tk.Frame(self.Frame4_6)
        self.Frame4_1.place(relx=1.962, rely=1.015, relheight=1.0, relwidth=1.0)
        self.Frame4_1.configure(relief='raised')
        self.Frame4_1.configure(borderwidth="2")
        self.Frame4_1.configure(relief="raised")
        self.Frame4_1.configure(background="#ffffff")
        self.Frame4_1.configure(highlightbackground="#d9d9d9")
        self.Frame4_1.configure(highlightcolor="#000000")
        self.Frame4_1.configure(highlightthickness="2")
        self.Frame4_1.configure(width=125)

        self.Frame5 = tk.Frame(top)
        self.Frame5.place(relx=0.24, rely=0.142, relheight=0.086, relwidth=0.072)

        self.Frame5.configure(relief='raised')
        self.Frame5.configure(borderwidth="2")
        self.Frame5.configure(relief="raised")
        self.Frame5.configure(background="#06b0c5")
        self.Frame5.configure(highlightbackground="#d9d9d9")
        self.Frame5.configure(highlightthickness="1")
        self.Frame5.configure(width=105)

        self.Frame5_7 = tk.Frame(top)
        self.Frame5_7.place(relx=0.756, rely=0.142, relheight=0.086
                            , relwidth=0.072)
        self.Frame5_7.configure(relief='raised')
        self.Frame5_7.configure(borderwidth="2")
        self.Frame5_7.configure(relief="raised")
        self.Frame5_7.configure(background="#e74d48")
        self.Frame5_7.configure(highlightbackground="#d9d9d9")
        self.Frame5_7.configure(highlightthickness="1")
        self.Frame5_7.configure(width=105)

        self.Frame5_8 = tk.Frame(top)
        self.Frame5_8.place(relx=0.498, rely=0.142, relheight=0.086
                            , relwidth=0.072)
        self.Frame5_8.configure(relief='raised')
        self.Frame5_8.configure(borderwidth="2")
        self.Frame5_8.configure(relief="raised")
        self.Frame5_8.configure(background="#439c47")
        self.Frame5_8.configure(highlightbackground="#d9d9d9")
        self.Frame5_8.configure(highlightthickness="1")
        self.Frame5_8.configure(width=105)


if __name__ == '__main__':
    vp_start_gui()