## 📌 Extended Project Description

This is a professional **Laboratory Information System (LIS) Middleware** application built using **C#** and **Windows Forms (Guna UI2 framework)**, backed by a **SQL Server** database. 

The software serves as a bridge between various laboratory medical analyzers and a centralized laboratory cloud database via a secure REST API.

### 🔌 Connectivity & Protocols
The application is capable of managing data streams from multiple modern and legacy analyzers simultaneously:
* **TCP/IP Network Client:** Successfully handles live socket connections with biochemistry and medical systems (e.g., *BK-280 BIOBASE*, *MS-H655*).
* **RS-232 Serial Communication (COM Ports):** Integrated serial communication with legacy urine analyzers (e.g., *US00 Mission Expert*), managing baud rates, parity, and data bits via `System.IO.Ports`.

### 🚀 Key Features
* **Real-time Dashboard:** Displays live system status, analyzer connection states, and a log of the latest test results.
* **Patient Records Management:** Structured UI for searching, adding, and viewing patient data and historical lab results.
* **Automated Data Processing:** Parses raw ASTM/HL7-like data streams from devices directly into structured database records.
* **Fail-Safe Data Transfer Queue:** Monitors and pushes data to the central web application API (`://labapp.com`). Includes built-in error handling and a pending retry queue for failed transfers.
<img width="2560" height="1080" alt="Screenshot (366)" src="https://github.com/user-attachments/assets/0c1b7628-aa8d-4486-a5e1-d978bc338fad" />
<img width="2560" height="1080" alt="Screenshot (370)" src="https://github.com/user-attachments/assets/66f93e01-0675-4c68-8071-8ea42252005b" />
<img width="2560" height="1080" alt="Screenshot (369)" src="https://github.com/user-attachments/assets/3e31a323-813c-4d49-9d26-a4a1a05b1af0" />
<img width="2560" height="1080" alt="Screenshot (368)" src="https://github.com/user-attachments/assets/8a8b275a-0fe2-4bd0-9e31-099612f3a59b" />
<img width="2560" height="1080" alt="Screenshot (367)" src="https://github.com/user-attachments/assets/c73238d9-f096-4b74-925f-c2760510aae6" />


<img width="2560" height="1080" alt="Screenshot (375)" src="https://github.com/user-attachments/assets/3d1df807-763b-4ef9-a749-9f5933887ac5" />
<img width="2560" height="1080" alt="Screenshot (374)" src="https://github.com/user-attachments/assets/f050fd9b-ef2e-4cff-b43d-19b32f1bc3e2" />
<img width="2560" height="1080" alt="Screenshot (373)" src="https://github.com/user-attachments/assets/a3e6ff3f-3ed6-4af8-9265-a9f300ca2332" />
<img width="2560" height="1080" alt="Screenshot (372)" src="https://github.com/user-attachments/assets/f2ec7cb5-813f-4717-91a3-513e2649fbd9" />
<img width="2560" height="1080" alt="Screenshot (371)" src="https://github.com/user-attachments/assets/08537653-9783-4b5e-bba5-5035222b0e52" />
