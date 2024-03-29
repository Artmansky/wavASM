;Project title: wavASM
;Algorithm adds the values of both the right and left channels of a WAV audio file and divides this sum by the number of channels (which is 2).
;The result is then saved back to the left channel.
;The code generates a new header, which is necessary for the WAV file to function, and saves the new MONO data to a new file.
;
;Time: 1 term, 3 year
;Author: Tomasz Artmanski
;Version: 1.0

; rcx/xmm0 - array of shorts containing values of the left channel
; rdx/xmm1 - array of shorts containing values of the right channel
; r8 - int value where designated array starts
; r9 - int value where designated array ends
; r11 - iterator of the array
; r12 - real starting point in array in assembly code

.code
ASMtoMONO proc
    ; iterator set to 0
    mov r11, 0 

    ; calculating size of an array
    sub r9, r8

    ; calculating starting point of an array, multiply value because data is given as short int
    mov r12, r8
    imul r12, 4

    ; moving pointers to starting point of the arrays
    add rcx, r12
    add rdx, r12
mainLoop:
    ; if iterator is in the end of an array, jump to end
    cmp r11, r9
    jge endLoop

    ; reading data from the array and putting it into xmm register
    movd xmm0, dword ptr [rcx + r11*4]
    movd xmm1, dword ptr [rdx + r11*4]

    ; summ of elements from both channels
    addps xmm0, xmm1
    
    ; divide it by 2
    psrad xmm0, 1

    ;store it back into memory
    movd dword ptr [rcx + r11*4], xmm0

    ; incrementing the iterator
    inc r11
    jmp mainLoop
endLoop:
    ret
ASMtoMONO endp
end