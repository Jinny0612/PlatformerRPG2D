# PlatformerRPG2D


## 1.关于生成enemy血条的问题

首先Canvas的Render Mode需要设置为Screen Space - Overlay

- Screen Space - Overlay：此模式下，UI 是直接在屏幕上渲染的，UI 坐标是基于屏幕空间的。
- Screen Space - Camera：此模式下，UI 是在摄像机视图中渲染的，UI 坐标系是相对摄像机的。
- World Space：此模式下，UI 是世界空间中的物体，位置和缩放是基于世界坐标的。

需要将Canvas的分辨率设置为与Camera相同，否则Camera.main.WorldToScreenPoint计算出的屏幕坐标可能会出现问题。

ScreenPoint坐标范围等于设置的分辨率；
ViewPortPoint为视窗坐标，(0,0)为屏幕左下角，屏幕右上角坐标为(1,1),屏幕中心坐标为(0.5,0.5)。

如果需要通过viewportpoint的偏移来确定坐标，需要注意偏移量的大小