# Job-Seeking Life Simulation Game
开发一个3D的养成游戏，具体内容暂定为养成求职类\
### 12.26 
\
实现人物的移动和相机跟随旋转\
完成了位移，摄像机\
增加了怪物，怪物头上会显示血条\

### 12.28
\
处理怪物状态机，使怪物有追击攻击巡逻的动画表现\
bug:: （使用了Layer但是忘记设置权重导致动画不播放。。。)\
下一步:: 修改角色转向，加快转向，主角攻击判定。伤害数字显示，暴击的伤害有不一样的显示\
\
\

## 一些想法：
如果是事件推动时间增长的话可以实现天空盒切换效果，使清晨中午晚上的天空盒都不同\
如果时间与事件不相关的话就不能直接切换，很生硬。可以程序实现一个无缝切换的天空盒
