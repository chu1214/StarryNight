# **⭐ Starry Night : 멀기만 했던 별들과 가까워 지는 밤**

![starrynight](https://user-images.githubusercontent.com/90178949/206023738-2d0de804-becd-4a5e-9c95-a8d2090a2350.png)

<img src="./exec/gif/star-information.gif"  width="800" height="400"/>

- 별자리 관측
    - 세계 15개국의 실시간 밤하늘을 메타버스 내에서 다른 사람들과 동시에 볼 수 있습니다.
    - 별을 클릭하여 자세한 정보를 볼 수 있습니다.
    - 별은 데이터를 기반으로 실제 위치에 밝기에 비례한 크기와 적절한 색으로 생성됩니다.
    - 불꽃놀이, 특수 효과 등 멀티플레잉을 즐길 수 있는 요소가 있습니다.
- 별자리 신화 게임
    - 공인된 88개 별자리 중 5개 별자리 신화를 미니게임을 해결하며 자연스럽게 익힐 수 있습니다.
    - 서비스에서 추가적으로 사용할 수 있는 스타코인을 수집할 수 있습니다.
- 다락방 꾸미기
    - 별자리 신화 게임에서 수집한 스타코인을 통해 웹에서 자신만의 방을 꾸밀 수 있습니다.
    - 유성타기 기능을 통해 다른 사람들의 다락방을 구경할 수 있습니다.

## 📀 서비스 소개 영상
[<img src="https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=YouTube&logoColor=white">](https://www.youtube.com/watch?v=eVtlH42FHsQ)
---

## 📖 주제

실시간 천체 위치를 반영한 PC + VR 기반 멀티플레잉 **플라네타리움** + 별자리 신화들을 바탕으로 한 **3D 게임** + 웹 기반 **다락방 꾸미기**

---

## 📅 **프로젝트 진행 기간**

2022.10.10(월) ~ 2022.11.20(일) [****42****일간 진행] - ****🏆 SSAFY 7기 2학기 자율프로젝트 1위 | 결선 입상 🏆****

---

## 🤔 기획 배경

- **빛 공해**
    - 대한민국은 빛 공해 세계 2위, 국토의 89.4%가 빛 공해 지역입니다.
    - 별 관측을 위해 장소, 시간, 환경, 장비 등 조건을 갖추지 않으면 별을 보기 힘든 시대가 되었습니다.
    - 별과 멀어진 현대 도시인들을 위해 별과 가까워 질 수 있는 서비스를 기획하게 되었습니다.

- **별과 별자리에 대한 관심**
    - 별과 별자리에 대해 잘 알지 못하는 사람들을 위해 별자리 설화 및 실시간으로 이동하는 별의 정보들을 제공하는 교육적인 컨텐츠를 기획하게 되었습니다.

- **활용성**
    - 우주과학에 대한 관심 증대
    - 실시간 별 관측 오픈소스 배포를 통한 다양한 외부 컨텐츠 개발에 기여
    - 학교, 과학관 등 교육적 체험 컨텐츠를 제공
    

---

## 🥌 서비스 특징

- **실시간 별 관측**
    - 별의 정보
        - **적경**, **적위**, **시등급**, **색깔**이 포함된 별 데이터를 제공
        - 시등급으로 별의 크기 지정 & 실제 별의 색깔 반영
        
    - 위치에 따른 밤하늘 변경
        - **15**개의 도시에서 별과 별자리를 보여줌
        - 현재 시간을 **해당 지역의 시간**으로 변경
        - 지역의 **위도**, **경도**, **시간**으로 천구의 회전량 계산
        - 회전량 만큼 천구 회전
        - → 지구의 자전에 따라서 실시간으로 회전하는 별 구현
        
- **밤하늘 보기**
    - 맵
        - 모닥불, 풀, 텐트 등 다양한 오브젝트들 및 풀벌레 소리, 모닥불 소리, 다양한 백색소음을 사용하여 현장에서 별을 보는 느낌을 제공
    - 사용자들끼리 즐길 수 있는 **불꽃놀이**, **스타 찾기** 컨텐츠 제공
        - 스타 찾기: 아이템을 먹어 특수 능력(점프 강화, 스피드 강화, 거대화, 밝게 빛나는 효과)를 발휘
    - 사용자간 **텍스트 채팅**, **음성 채팅** 기능 제공
    - **멀티플레이** 제공
    - **VR**(Oculus quest 2(VR 기기)를 통해 밤하늘 보기를 플레이 가능)
    - **Blender**를 통해 모델링한 오브젝트 구현
    
- **신화 게임**
    - 5개의 별자리 스토리
        - 사자자리, 처녀자리, 페르세우스자리, 카시오페아자리, 거문고자리
        - 한 스토리당 플레이 타임 **10분**을 기준으로 함
        
    - 스토리마다 제공되는 미니 게임
        - 사물과 상호작용, 미로 탈출, 공포 게임, 전투, 점프맵 등 다양한 **미니게임**을 통해 스토리 각각의 개성을 주어 지루하지 않는 플레이 제공
        
    - 10개의 다양한 맵
        - asset을 사용후 리모델링을 통해 **사용자의 이동거리를 고려**
        - 시간에 따른 skybox 변화, 날씨 환경에 따른 맵 오브젝트 변화를 주어 맵들에 **각기 다른 성격**을 담아냄
        - **성능 저하를 고려**하여 collider 설정을 mesh → box 로 변경
        - 탈출을 해야하는 넓은 미로 맵의 경우 난이도와 플레이 타임을 조절하기 위해 미니맵을 제공
    
    - 22개의 퀘스트
        - 별자리 설화를 퀘스트의 내용에 담아 사용자가 주인공을 돕는 서브 주인공으로 플레이
    
    - 별(재화)
        - 추가 컨텐츠 다락방을 꾸밀 수 있는 재화를 맵 곳곳에 숨겨 **맵을 탐방할 수 있는 요소**를 추가
    
    - 사운드
        - 배경, 캐릭터, 전투 및 각종 오브젝트에 대한 사운드 제공
        - 자연스러운 연출을 위하여 **애니메이션 사이에 event 함수를 생성**하여 사운드를 부여
    
    - 시야
        - 일반적인 맵은 **3인칭**, 미로가 동반된 공포게임의 경우 몰입감을 위하여 시야가 제한된 **1인칭** 제공
    
    - 애니메이션
        - NPC
            - 플레이어를 따라가는 NPC를 **navmeshagent**를 사용하여 구현
        - 적
            - Idle, Patrol, Chase, Attack 상태로 나누어 플레이어와의 기본적인 전투를 구현

- **다락방**
    - 게임에서 획득한 별(재화)을 통해 가구를 구매하여 자신만의 다락방을 꾸미기 가능
    - **유성타기**를 통하여 다른 사용자들의 다락방을 구경 가능

---

## 📝 **주요 기능**

### 회원 관리

- 카카오, 구글 **소셜로그인** 제공
- 닉네임 변경 가능

### 메인페이지

- 제공하는 기능을 한 눈에 볼 수 있게 페이지 단위로 설명을 제공
- 백그라운드에 신화 게임의 별자리들을 그려 그 위에 획득한 별들을 빛나게 만들어 **게임의 진행 사항을 한눈에 표시**

### 책 펼치기

- **별 보러 가기**를 통해 신화 게임 플레이
- **신화 읽기**를 통해 별자리 신화에 대한 자세한 이야기 및 정보 제공
- **튜토리얼**을 제공하여 게임에서 필요한 조작법 및 가이드 제공

### 밤하늘 보기

- **멀티플레이**를 제공(PC & VR)
- (**PC, VR) 버전**을 다운로드하여 밤하늘 보기를 플레이 가능
- **튜토리얼**을 통해 게임 다운로드, 컨텐츠 소개, 조작 방법을 상세히 제공

### 방 꾸미기

- 현재 가지고 있는 가구들을 이용하여 다락방을 꾸밀 수 있는 기능 제공
- **Drag & Drop**을 통한 편리한 배치, 가구 회전 기능 제공
- **유성타기**를 통해 다른 무작위 사용자의 방을 구경할 수 있음

### 상점가기

- 신화 게임 속 획득한 별들을 이용하여 **180**가지 이상의 가구를 구매 가능

---

## ⌨️ **주요 기술**

**Backend**

- IntelliJ IDE 2022.1.3(Ultimate Edition) 11.0.15 + 10-b2043.56 amd64
- Spring Security
- Query DSL
- Swagger
- Hibernate
- JWT
- SpringBoot

**Frontend**

- Node.js
- React 18.2.0
- Recoil 0.7.5
- Tailwind CSS
- TypeScript

**Unity**

- Unity 2021.3.11f
- Photon Unity Networking 2.41
- XR Interaction Tool Kit

**CI/CD**

- AWS EC2 Ubuntu 20.04 LTS
- Jenkins 2.361.3
- NGINX 1.23.2
- SSL 인증서
- MySQL
- gabia

---

## 🔧 **협업 툴**

- GitLab
- Notion
- JIRA
- MatterMost
- Webex
- Discord
- Gather Town

---

## 🏗️ **프로젝트 파일 구조**

### **Backend**

```markdown
backend
├─gradle
│  └─wrapper
└─src
    ├─main
    │  ├─java
    │  │  └─starrynight
    │  │      ├─api
    │  │      │  ├─controller
    │  │      │  ├─dto
    │  │      │  │  ├─game
    │  │      │  │  ├─member
    │  │      │  │  ├─room
    │  │      │  │  └─store
    │  │      │  └─service
    │  │      ├─config
    │  │      │  ├─auth
    │  │      │  ├─jwt
    │  │      │  ├─querydsl
    │  │      │  └─swagger
    │  │      ├─db
    │  │      │  ├─entity
    │  │      │  └─repository
    │  │      ├─enums
    │  │      ├─exception
    │  │      └─interceptor
    │  └─resources
    └─test
        └─java
            └─starrynight
```

### **Frontend**

```markdown
.
├── public
│   ├── Build
│   │   ├── cassiopeia
│   │   ├── leo
│   │   ├── lyra
│   │   ├── perseus
│   │   └── virgo
│   ├── Download
│   └── assets
│       ├── constellation
│       ├── furniture
│       ├── main
│       ├── others
│       ├── team
│       ├── thumbnail
│       └── tutorial
└── src
    ├── api
    ├── components
    │   ├── book
    │   ├── myroom
    │   ├── navbar
    │   └── tutorial
    ├── pages
    ├── recoil
    │   └── member
    └── utils
```

---

## 👥 **팀원 주요 역할**

### BackEnd

### 👩🏻‍💻 신슬기 - 팀장, Game

### 👨🏻‍💻 황승주 - VR, DevOps

### 👨🏻‍💻 박희조 - Game, UCC

### FrontEnd

### 👩🏻‍💻 안지영 - Web, VR

### 👩🏻‍💻 채송지 - Web, Design

### 👨🏻‍💻 박종민 - Game

---

## **✔ 프로젝트 산출물**

[ERD](https://github.com/chu1214/StarryNight/blob/main/exec/ERD.PNG)

[포팅 매뉴얼](https://github.com/chu1214/StarryNight/blob/main/exec/%ED%8F%AC%ED%8C%85%EB%A7%A4%EB%89%B4%EC%96%BC_StarryNight.pdf)

[시스템 아키텍쳐](https://github.com/chu1214/StarryNight/blob/main/exec/%EC%8B%9C%EC%8A%A4%ED%85%9C%20%EC%95%84%ED%82%A4%ED%85%8D%EC%B3%90.png)

[시연 시나리오](https://github.com/chu1214/StarryNight/blob/main/exec/%EC%8B%9C%EC%97%B0_%EC%8B%9C%EB%82%98%EB%A6%AC%EC%98%A4.pdf)

---

## 🖥️ **서비스 화면**

### 메인페이지

- Starry Night에서 제공하는 서비스들을 한눈에 볼 수 있습니다.

<img src="./exec/gif/mainpage.png"  width="800" height="400"/>

<img src="./exec/gif/mainpage2.png"  width="800" height="400"/>

<img src="./exec/gif/mainpage3.png"  width="800" height="400"/>

<img src="./exec/gif/mainpage4.png"  width="800" height="400"/>

<img src="./exec/gif/mainpage5.png"  width="800" height="400"/>

<img src="./exec/gif/mainpage6.png"  width="800" height="400"/>

### 로그인

- 카카오 & 구글 소셜 로그인을 제공

<img src="./exec/gif/login.png"  width="800" height="400"/>

### 다락방

- 메인 서비스 3가지를 이용할 수 있음
    - **신화 게임**
    - **밤하늘 보기**
    - **방 꾸미기**

<img src="./exec/gif/main-room.png"  width="800" height="400"/>

- **유성 타기** - 다른 사용자들의 다락방을 구경할 수 있음

<img src="./exec/gif/other-room.gif"  width="800" height="400"/>


### 책 펼치기

- 책 펼치기 - 신화 게임들을 플레이할 수 있는 공간

<img src="./exec/gif/book.png"  width="800" height="400"/>

- 신화 읽기 - 클리어한 신화 게임의 별자리에 대한 정보들을 제공

<img src="./exec/gif/readMyth.png"  width="800" height="400"/>

- 튜토리얼 - 신화 게임의 조작법을 설명하는 게임 가이드

<img src="./exec/gif/tutorial.png"  width="800" height="400"/>

### 신화 게임

- 별자리 신화 관련 퀘스트

<img src="./exec/gif/quest.gif"  width="800" height="400"/>

- 처녀자리 - 물체와 상호작용

<img src="./exec/gif/virgo.gif"  width="800" height="400"/>

- 사자자리 - 미니게임(전투)

<img src="./exec/gif/lion.gif"  width="800" height="400"/>

- 페르세우스자리 - 미니게임(공포게임)

<img src="./exec/gif/perseus.gif"  width="800" height="400"/>

- 카시오페이아자리 - 미니게임(미로)

<img src="./exec/gif/cassiopeia.gif"  width="800" height="400"/>

- 거문고자리 - 미니게임(점프맵)

<img src="./exec/gif/lyra.gif"  width="800" height="400"/>

### 밤하늘 보기

- PC로 별 보기 / VR로 별 보기 - 2가지 모드로 플레이 가능

<img src="./exec/gif/1.png"  width="800" height="400"/>

- 튜토리얼 - (다운로드, 컨텐츠 소개, 조작 방법)에 관련된 정보를 사용자에게 제공)

<img src="./exec/gif/2.png"  width="800" height="400"/>

- 불꽃 놀이

<img src="./exec/gif/firework.gif"  width="800" height="400"/>

- 스타(아이템) 획득을 통한 특수능력 부여

<img src="./exec/gif/star-item.gif"  width="800" height="400"/>

- 도시 변경으로 별자리 위치 변경

<img src="./exec/gif/change-star.gif"  width="800" height="400"/>

<img src="./exec/gif/selectCity.gif"  width="800" height="400"/>

- 별자리 정보 ON/OFF

<img src="./exec/gif/onOffInfo.gif"  width="800" height="400"/>

- 별을 클릭하여 별의 자세한 정보 확인

<img src="./exec/gif/star-information.gif"  width="800" height="400"/>

### 상점 가기

- 신화 게임에서 얻은 재화로 방을 꾸밀 수 있는 아이템을 구매 가능

<img src="./exec/gif/shop-buy.gif"  width="800" height="400"/>

### 방 꾸미기

- 구매한 가구를 이용하여 자신의 다락방을 꾸밀 수 있습니다.

<img src="./exec/gif/change-room.gif"  width="800" height="400"/>
