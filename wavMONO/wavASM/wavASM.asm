.code
ASMtoMONO proc

	;size
	mov rbx, r8
	mov r11, rbx

mainLoop:
	cmp r11, 0
	je endLoop

	movdqu xmm0, oword ptr[rcx]
	
	movdqu xmm1, oword ptr[rdx]

	punpcklwd xmm0, xmm0 ; Unpack low words (shorts) to integers
    punpcklwd xmm1, xmm1 ; Unpack low words (shorts) to integers

	paddsw xmm0, xmm1 ; Add packed signed words

	psrldq xmm0, 2 ; Shift 2 bytes (1 short)

	packuswb xmm0, xmm0 ; Pack result to words (shorts)

    movdqu oword ptr [rcx], xmm0

	add rcx, 16
	add rdx, 16
	sub r11, 8
	jmp mainLoop
endLoop:
	ret
ASMtoMONO endp
end
