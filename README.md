# TowerDefense

Unity Version 2017.3.0f3  

IGME 450.01.02  
Team Name: heckyes  
Members: Jia Wessen, Sungmin Park, Zach Michaels, Corinne Green  
Concept: A top down build-your-own defense game.

# Git / Github info

## Clone

When you clone a repo it will create a folder with repo's name Plants. It will have a .git file in it which will track any changes to the folder.  
The unity files will be in `Plants\Plants\`. **Leave this as is to prevent adding local settings to the repo.**  

## Branch

You can use Git GUI like source tree to do these features for you.  

**Command line**
*	Create new branch `git checkout -b (branchName)`
*	View local branches and current branch `git branch`
*	Change local branch `git checkout (branchName)`
*	Delete local branch `git branch -d (branchName)`


## Staging, Commiting, and Pushing

You can use Git GUI like source tree to do these features for you.  

**Command line**  
*	Discard Changes `git checkout /file/path/to/revert` or `git checkout .` discard all file changes
*	Check status `git status`
*	Stage changes `git add (file name)` or `git add --a` to add all changed files
*	Commit `git commit -m '(commit message)`
*	Push `git push (remote) (branch)` remote will generally be `origin`


## Pull Request

When your branch is ready to be merged create a pull request.  

1. Click Pull Requests Tab
2. Click Green New Pull Request Button
3. Select correct branch in compare:(branch name) button. Leave Base: master as is.
4. Git will tell you if it can be auto merged.
5. Fix any merge conflicts mentioned or let Sungmin know and he will get it fixed (**need to know what your branch is intended to do**)
6. When auto merge allowed, merge into master **as squash and merge**
7. Check master to see if its working as expected
8. If its not working as expected rebase master as last commit or let Sungmin know and he will look into undoing the merge.
