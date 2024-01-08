;Project title: wavASM
;Algorithm adds the values of both the right and left channels of a WAV audio file and divides this sum by the number of channels (which is 2).
;The result is then saved back to the left channel.
;The code generates a new header, which is necessary for the WAV file to function, and saves the new MONO data to a new file.
;
;Time: 1 term, 3 years
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
    imul r12, 2

    ; moving pointers to starting point of the arrays
    add rcx, r12
    add rdx, r12

mainLoop:
    ; if iterator is in the end of an array, jump to end
    cmp r11, r9
    jge endLoop

    ; reading data from the array and putting it into xmm register
    movq xmm0, qword ptr [rcx + r11*2]
    movq xmm1, qword ptr [rdx + r11*2]

    ; incrementing the iterator
    inc r11
    jmp mainLoop

endLoop:
    ; summ of elements from both channels
    addps xmm0, xmm1

    ; deviding by 2 using bitwise move to right
    psrldq xmm0, 1

    ; saving result to the left channel
    movq qword ptr [rcx], xmm0
    ret
ASMtoMONO endp
end