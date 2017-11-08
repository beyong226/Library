# BookLibrary
NET Core 2.0 + DDD + CQRS + ES + RabbitMQ练习项目

## 系统架构图
![系统架构图](https://github.com/lamondlu/BookLibrary/blob/master/Documents/Architecture/20171107104353.png)

## 事件驱动架构
![事件驱动架构图](https://github.com/lamondlu/BookLibrary/blob/master/Documents/Architecture/20171108152513.png)

## 服务说明
系统中提供会员服务，书籍库存服务，书籍租赁服务

### 会员服务
* 提供管理员及会员的登录
* 管理员可以添加, 修改会员信息
* 管理员删除会员时，需要检查会员是否有未归还的书籍，如果有，则不能删除
* 管理员和会员可以修改自己的个人信息，修改密码

### 书籍库存服务
* 管理员可以管理书籍（新增，修改，删除）
* 管理员可以批量添加书籍库存（暂时硬编码1次10本）
* 书籍拥有以下属性
    *    书籍名称(必填)
    *    ISBN(必填)
    *    描述
    *    上架时间(必填)
* ISBN是系统中是唯一的，如果新增/修改书籍，需要保证ISBN系统唯一
* 书籍库存有2种状态，出库/入库。
  
### 书籍租赁服务
* 已租借状态的书籍不能再次租借
* 会员每次只能租借最多3本书
