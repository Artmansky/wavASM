.code
ASMtoMONO proc
    ; wyzerowanie licznika
    mov r11, 0 

    ; obliczenie rozmiaru fragmentu do przetworzenia
    sub r9, r8

    ; obliczenie rozmiaru fragmentu w bajtach (2 razy rozmiar bo short int)
    mov r12, r8
    imul r12, 2

    ; przesuni�cie wska�nik�w o pocz�tek fragmentu
    add rcx, r12
    add rdx, r12

mainLoop:
    ; je�eli licznik jest r�wny rozmiarowi fragmentu, koniec
    cmp r11, r9
    jge endLoop

    ; wczytanie lewego i prawego kana�u audio
    movq xmm0, qword ptr [rcx + r11*2]
    movq xmm1, qword ptr [rdx + r11*2]  

    ; inkrementacja licznika
    inc r11
    jmp mainLoop

endLoop:
    ; dodanie lewego i prawego kana�u
    paddw xmm0, xmm1

    ; przesuni�cie wyniku sumowania o po�ow� (dzielenie przez 2)
    psrldq xmm0, 1

    ; zapisanie wyniku operacji w lewym kanale
    movq qword ptr [rcx], xmm0
    ret

ASMtoMONO endp
end