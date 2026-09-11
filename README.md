# Real-Time PoE Network-Based Vehicle Access Control System

## Overview
The Real-Time PoE Network-Based Vehicle Access Control System is a capstone project developed for Batangas State University – The National Engineering University, Alangilan Campus.
The system was designed to automate vehicle entry and exit monitoring by using image processing and Optical Character Recognition (OCR) to detect and recognize vehicle license plates. Recognized plate information is processed through a network-based system and can be monitored through a web-based dashboard.
The project combines a PoE camera network, YOLO-based license plate detection, OCR, ASP.NET Web API, Python Flask, and Microsoft SQL Server.

## Project Objectives
- Detect vehicle license plates in real time.
- Extract plate numbers using Optical Character Recognition (OCR).
- Validate vehicles against registered vehicle information.
- Automate vehicle entrance and exit monitoring.
- Provide a web-based dashboard for vehicle registration and monitoring.
- Reduce dependence on manual vehicle logging and checking.

## System Architecture

```text
PoE Cameras
     |
     v
PoE Network / LAN
     |
     v
Image Processing (YOLO)
     |
     v
License Plate Detection
     |
     v
OCR (EasyOCR)
     |
     v
Python Flask / SocketIO
     |
     v
ASP.NET Web API
     |
     v
Microsoft SQL Server
     |
     v
Web-Based Dashboard
```

## Backend

The backend provides the API layer responsible for receiving and processing vehicle-related data and communicating with the database and other system components.

### Main Backend Responsibilities

- Receive vehicle and plate recognition data. 
- Process vehicle access information. 
- Communicate with the database. 
- Support vehicle registration and management. 
- Handle entrance and exit logs. 
- Provide data to the web-based dashboard. 
- Validate recognized vehicle information. 

## Technologies Used

### Backend

- **C#** 
- **ASP.NET Web API** 
- **.NET 8** 
- **Python Flask** 
- **Socket.IO** 
- **SignalR** 

### Database

- **Microsoft SQL Server** 
- **SQL Server Management Studio (SSMS)** 

### Image Processing and OCR

- **YOLO / YOLOv11** — license plate detection 
- **EasyOCR** — license plate character recognition 
- **SORT** — vehicle tracking 

### Network and Hardware

- PoE IP Cameras 
- PoE Switch 
- LAN / Ethernet Cabling 
- Desktop computer for processing 

### Frontend

- HTML 
- CSS 
- JavaScript 

## Key Features

### Vehicle Registration
Administrators can register vehicle information, including license plate details and vehicle owner information.

### License Plate Detection
The system uses YOLO-based image processing to locate vehicle license plates from camera feeds.

### Optical Character Recognition
EasyOCR extracts the alphanumeric characters from detected license plates.

### Vehicle Validation
Recognized plate numbers can be compared with registered vehicle information to determine whether a vehicle is recognized or unrecognized.

### Entrance and Exit Monitoring
The system records vehicle activity at campus gates and supports monitoring of entrance and exit logs.

### Real-Time Monitoring
The system uses network communication and real-time technologies such as Flask/Socket.IO and SignalR to provide updated vehicle information to the dashboard.

## Camera Configuration

The system was designed around a four-camera configuration to support the changing traffic direction of Gate 1 and Gate 3.

- **During the daytime:**
  - Gate 1 — Entrance 
  - Gate 3 — Exit 

- **During nighttime:**
  - Gate 1 — Exit 
  - Gate 3 — Entrance 

The four-camera configuration was selected to ensure that both gates could be monitored regardless of the scheduled traffic direction.

## Performance

Based on the project's testing:

| Component | Result |
| --- | --- |
| YOLOv11 mAP@0.5 | 98% |
| Character Recognition Rate (CRR) | 89% |
| Plate Recognition Rate (PRR) | 87.22% |
| PoE Camera Performance | 18 FPS |
| End-to-End Latency | 225 ms |

The OCR stage was identified as the main performance bottleneck, contributing approximately 150 ms to the measured processing latency.

## Project Limitations

The project identified several limitations during testing and integration:

- OCR processing introduces additional latency. 
- Poor lighting, weather conditions, motion blur, and camera angles can affect recognition. 
- Vehicles positioned too far from the camera are more difficult to detect accurately. 
- Network quality and long LAN cable distances can affect data transmission. 
- Integration between the Python and C# components requires consistent API data contracts. 
- Duplicate vehicle logs can occur without sufficient debounce or vehicle-state logic. 
- The system was designed primarily for the campus access-control requirements and does not integrate with external security databases. 

## Known Integration Issues

Testing identified issues involving the communication between the Python ALPR component and the C# backend:

1. **Data Type Mismatch**: Some Python output was returned in a format that did not match the expected C# API data type. 
2. **API Contract Mismatch**: Some field names used by the Python component did not match the fields expected by the C# API. 
3. **Duplicate Logging**: Continuous detection of a stationary vehicle could generate repeated logs without proper debounce or vehicle-state validation. 

These issues are documented as areas for future improvement rather than being hidden from the project's implementation history.

## Future Improvements

Possible improvements include:

- Standardize the Python-to-C# API contract. 
- Clean and validate OCR output before sending it to the API. 
- Implement vehicle-state and debounce logic to prevent duplicate logs. 
- Improve real-time database lookup and vehicle status validation. 
- Improve image quality before OCR processing using image enhancement or super-resolution techniques. 
- Evaluate alternative OCR solutions for better accuracy and processing speed. 
- Improve scalability for higher vehicle traffic. 

## Project Context

This project was developed as a capstone project for the **Bachelor of Science in Information Technology with Specialization Track in Network Technology** at Batangas State University – The National Engineering University.

The project focused on applying networking, backend development, computer vision, OCR, database management, and web technologies to a real-world campus security and vehicle monitoring problem.

## Team

- **Aron Joshua M. Holgado** 
- **Rein Derick L. Salosa** 
- **Lorvin R. Santos** 

**Adviser:** Assoc. Prof. Lloyd H. Macatangay

## Documentation

The complete capstone manuscript contains the project's:

- Background and objectives 
- Related literature and studies 
- System design and methodology 
- Hardware and software implementation 
- Testing and evaluation 
- Results and discussion 
- Conclusions and recommendations 

## License

This project was developed as an academic capstone project. Please contact the project authors before using or redistributing the source code for other purposes.
