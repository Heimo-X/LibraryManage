
# 🧠 Git 指令总结笔记（中英对照）

## 🚀 初始化与配置

| 命令 | 说明 |
|------|------|
| `git init` | 初始化本地 Git 仓库 Initialize a local Git repository |
| `git config --global user.name "你的名字"` | 设置用户名 Set global Git username |
| `git config --global user.email "你的邮箱"` | 设置邮箱 Set global Git email |
| `git --version` | 查看 Git 版本 Check installed Git version |

## 📁 版本控制基础

| 命令 | 说明 |
|------|------|
| `git add .` | 添加所有修改文件到暂存区 Stage all changes |
| `git commit -m "描述"` | 提交暂存区到本地仓库 Commit with message |
| `git commit --amend -m "新提交信息"` | 修改上一次提交的信息 Amend last commit message |
| `git log` | 查看提交历史 View commit history |

## ⏪ 回退操作

| 命令 | 说明 |
|------|------|
| `git reset --soft HEAD^` | 撤销最近一次提交，保留修改 Undo last commit, keep changes staged |
| `git reset --mixed HEAD^` | 撤销最近一次提交和 `add`，但保留文件修改 Undo commit and unstage files |
| `git reset --hard HEAD^` | 强制回退，撤销提交和修改 WARNING: 丢失修改 Undo and discard all changes |

## 🌿 分支管理

| 命令 | 说明 |
|------|------|
| `git branch` | 查看所有分支 List all branches |
| `git switch -c 新分支名` | 创建并切换到新分支 Create and switch to a new branch |
| `git switch 分支名` | 切换分支 Switch branch |

## 🌐 远程仓库操作

| 命令 | 说明 |
|------|------|
| `git remote add origin git@github.com:用户名/仓库名.git` | 添加远程仓库 Add remote repo |
| `git push -u origin main` | 首次推送并建立跟踪 Push to remote and set upstream |
| `git push` | 推送最新代码 Push current branch to remote |
| `git pull` | 拉取远程最新代码 Pull latest changes |
| `git clone 地址` | 克隆远程仓库 Clone a repository |

## 🔐 SSH 配置相关

| 命令 | 说明 |
|------|------|
| `ssh-keygen -t rsa -b 4096 -C "邮箱"` | 生成 SSH 密钥 Generate SSH key |
| `ssh-add ~/.ssh/id_rsa` | 添加私钥到 SSH 代理 Add private key |
| `ssh -T git@github.com` | 测试与 GitHub 的 SSH 连接 Test SSH connection |
| `ssh-add -l` | 查看已添加的私钥 List current identities |

## 📝 提交建议

- 提交信息建议中英对照，例如：  
  ```bash
  git commit -m "📚 添加序列化保存功能 | Add book serialization feature"
  ```

- 如果推送失败提示 `fetch first`，先拉取远程变更：  
  ```bash
  git pull --rebase origin main
  ```
