.code
ASMtoMONO proc
    ;wyzerowanie licznika
	mov r8, 0 

mainLoop:
    ;jezeli licznik jest rowny rozmiarowi tabeli koniec
	cmp r8, r11
    je endLoop

    ;ladowanie prawego i lewego kanalu audio
    movq xmm0, qword ptr [rcx + r8*2]
    movq xmm1, qword ptr [rdx + r8*2]  

    ;dodawanie do siebie prawego i lewego kanalu
    paddsw xmm0, xmm1

    ;dzielenie dodanych kanalow przez 2 poprzed przesuniecie w prawo o 1
    psrldq xmm0, 1

    ;wynik operacji zapisany w lewym kanale
    movq qword ptr [rcx + r8*2], xmm0

    ;inkrementacja licznika
    inc r8
    jmp mainLoop
endLoop:
	ret
ASMtoMONO endp
end
