import cv2
from cvzone.HandTrackingModule import HandDetector
import socket


width, height = 1280, 720

camera = cv2.VideoCapture(0)
camera.set(3,width)
camera.set(4,height)

detector = HandDetector(maxHands=1, detectionCon=0.8)


sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort  = ("127.0.0.1", 5052)


while True:
    success, image = camera.read()

    hands, image = detector.findHands(image)
    data = []
    if hands:
        hand = hands[0]
        landMarks = hand['lmList']
        for lm in landMarks:
            data.extend([lm[0], height-lm[1], lm[2]]) 
        data = (str(data)[1:-1]).replace(' ', '')
        sock.sendto(str.encode(data), serverAddressPort)
        print(data)

    cv2.imshow("Image", image)
    cv2.waitKey(15)
