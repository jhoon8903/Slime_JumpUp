# 🕴🏻 슬라임 점프 리팩토링 프로젝트

- 과거 슬라임 점프 게임을 객체지향 및 좀 더 발전된 코드로 리팩토링 시도
- 과거 게임 개발 자료 [링크]("https://github.com/jhoon8903/Unity_ClimbingGame") / [개발블로그]("https://jhoon8903.github.io/project/2023/05/04/%EA%B0%9C%EB%B0%9C%EC%99%84%EB%A3%8C.html")


## 🛠️ 리팩토링 내역

##### 📇 코드 객체지향화
##### 🗂️ Addressable을 사용한 리소스 관리
##### 🔖 UI Binder 를 사용한 UI 최적화
##### 🐒 과거 실패했던 3D 모델 캐릭터 교체
##### 📦 Object Pooling 방식 변경
##### 🎞️ Cinemachine 을 이용한 캐릭터 추적
##### 🖥️ Debounce 을 활용한 동적 해상도 변경


## 🐞 확인된 버그 내역 (23.12.29)

- ~~UI 바인딩 시 GameObject 를 바인딩 하지 못하는 문제~~
- 빌드시에 발생하는 GameScene Resource Load Skip
- 이미 사라진 장애물에 Access 하는 문제
- Character Joint 떨림 및 스트래치 되는 현상

## 👨🏻‍💻 개발 진행중인 사항

- Obstacle Spawn 시에 캐릭터 주위로만 Spawn 이 되도록
- Cinemachine 을 이용한 캐릭터 추적
- 장애물에 매달릴 수 있는 팔 다리의 최대 한도 설정
- 높이 변화에 따라 Background 설정 변경
- 플레이어를 방해하는 이벤트 오브젝트 추가

## 📝 개발 일지

- [빌드 에러]("https://jhoon8903.github.io/unity/2023/12/29/BuildError.html")
- [해상도 조절]("https://jhoon8903.github.io/unity/2023/12/28/WindowResolution.html")
- [UI Manager Refactoring]("https://jhoon8903.github.io/unity/2023/12/27/UIManagerRefactoring.html")
- [Object Pooling]("https://jhoon8903.github.io/unity/2023/12/26/Object-Pool.html")
- [Rag doll]("https://jhoon8903.github.io/unity/2023/12/25/Joint.html")
- [Service Locator](https://jhoon8903.github.io/unity/2023/12/24/ServiceLocator2.html)
- [Allocation Warning]("https://jhoon8903.github.io/unity/2023/12/23/UnityError.html")
- [Git LFS]("https://jhoon8903.github.io/github/2023/12/22/Git-LFS.html")


### 🖍️ 관계  Diagram

![](https://i.imgur.com/RxvMPp6.jpg)

### 📌 의존성 관계

#### Service Locator 를 이용한 의존성  ✂️ 완전 분리

![](https://i.imgur.com/v4mgrs8.jpg)


